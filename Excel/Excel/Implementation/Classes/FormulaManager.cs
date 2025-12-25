using Excel.Core.Abstraction;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Excel.Core.Implementation.Classes
{
    public class FormulaManager : IFormulaManager
    {
        private readonly ITable _table;
        private readonly IFunctionManager _functions;
        private readonly HashSet<(int, int)> _evalCache = new();

        public FormulaManager(ITable table, IFunctionManager functions)
        {
            _table = table;
            _functions = functions;
            _table.AddObserver(this);
        }

        public void OnCellChanged(int row, int col)
        {
            RecalculateAll();
        }

        public void RecalculateAll()
        {
            for (int r = 0; r < _table.RowCount; r++)
                for (int c = 0; c < _table.ColumnCount; c++)
                    EvaluateCell(r, c);
        }

        public object? EvaluateCell(int row, int col)
        {
            try
            {
                string raw = _table.GetRawValue(row, col)?.ToString() ?? "";
                if (!raw.StartsWith("=")) return ProcessLiteral(row, col, raw);

                string expression = raw[1..];

                // Detect Name(...) and resolve ranges inside
                foreach (var name in _functions.GetFunctionNames())
                {
                    string pattern = $@"\b{name}\(([^)]*)\)";
                    expression = Regex.Replace(expression, pattern, match =>
                    {
                        var args = ResolveArgs(match.Groups[1].Value);
                        var result = _functions.Get(name).Execute(args);

                        return Convert.ToString(result, CultureInfo.InvariantCulture) ?? "0";
                    });
                }

                // Replace remaining A1, B2 (outside functions)
                expression = Regex.Replace(expression, @"[A-Z]+\d+", m =>
                    GetCellValue(m.Value)?.ToString() ?? "0");

                var resultValue = new DataTable().Compute(expression, null);
                _table.SetDisplayValue(row, col, resultValue);
                return resultValue;
            }
            catch
            {
                _table.SetDisplayValue(row, col, "#ERR");
                return "#ERR";
            }
            finally { _evalCache.Remove((row, col)); }
        }

        private List<object?> ResolveArgs(string argsText)
        {
            var resolved = new List<object?>();
            var parts = argsText.Split(',').Select(p => p.Trim());

            foreach (var part in parts)
            {
                if (part.Contains(':')) // Handle ranges
                {
                    var range = part.Split(':');
                    var (r1, c1) = ParseCellId(range[0]);
                    var (r2, c2) = ParseCellId(range[1]);

                    for (int r = Math.Min(r1, r2); r <= Math.Max(r1, r2); r++)
                        for (int c = Math.Min(c1, c2); c <= Math.Max(c1, c2); c++)
                            resolved.Add(GetCellValue(r, c));
                }
                else if (Regex.IsMatch(part, @"^[A-Z]+\d+$")) // Handle single cell references
                {
                    resolved.Add(GetCellValue(part));
                }
                else // Handle Constants
                {
                    resolved.Add(double.TryParse(part, out var d) ? d : part);
                }
            }
            return resolved;
        }

        private object? GetCellValue(string cellId)
        {
            var (r, c) = ParseCellId(cellId);
            return GetCellValue(r, c);
        }

        private object? GetCellValue(int r, int c)
        {
            string raw = _table.GetRawValue(r, c)?.ToString() ?? "";
            object? val;

            if (raw.StartsWith("="))
                val = EvaluateCell(r, c);
            else
                val = _table.GetDisplayValue(r, c);

            // If the value is a number, return it as a string with a dot for the expression
            if (val is double d)
                return d.ToString(CultureInfo.InvariantCulture);

            return val;
        }

        private (int row, int col) ParseCellId(string id)
        {
            int col = 0, pos = 0;
            while (pos < id.Length && char.IsLetter(id[pos]))
                col = col * 26 + (char.ToUpper(id[pos++]) - 'A' + 1);
            return (int.Parse(id[pos..]) - 1, col - 1);
        }

        private object ProcessLiteral(int r, int c, string raw)
        {
            var val = double.TryParse(raw, out var n) ? n : (object)raw;
            _table.SetDisplayValue(r, c, val);
            return val;
        }
    }
}