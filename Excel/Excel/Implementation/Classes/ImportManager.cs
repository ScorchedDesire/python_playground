using Excel.Core.Abstraction;
using Excel.Core.Implementation.Entities;
using System.Text.Json;

namespace Excel.Core.Implementation.Classes
{
    public class ImportManager
    {
        // Imports a CSV file into an ITable
        public ITable ImportCsv(string path)
        {
            var lines = File.ReadAllLines(path);

            int rows = lines.Length;
            int cols = lines[0].Split(';').Length;

            ITable table = new Table(rows, cols);

            for (int r = 0; r < rows; r++)
            {
                var values = lines[r].Split(';');
                for (int c = 0; c < cols; c++)
                {
                    table.SetRawValue(r, c, values[c]);
                }
            }

            return table;
        }
        // Imports a JSON file into an ITable
        public ITable ImportJson(string path)
        {
            var json = File.ReadAllText(path);

            var rows = JsonSerializer.Deserialize<List<List<object?>>>(json)
                       ?? throw new Exception("Invalid JSON format");

            int rowCount = rows.Count;
            int colCount = rows[0].Count;

            var table = new Table(rowCount, colCount);

            for (int r = 0; r < rowCount; r++)
            {
                for (int c = 0; c < colCount; c++)
                {
                    var value = rows[r][c];
                    table.SetRawValue(r, c, value?.ToString());
                }
            }

            return table;
        }

        // Imports an Excel XML file into an ITable
        public ITable ImportXls(string path)
        {
            // treat Excel XML as plain text (values only)
            var xml = File.ReadAllText(path);

            // Extract <Data>...</Data> values
            var matches = System.Text.RegularExpressions.Regex.Matches(xml, "<Data[^>]*>(.*?)</Data>");

            // Count rows by counting <Row> tags
            int rows = System.Text.RegularExpressions.Regex.Matches(xml, "<Row>").Count;
            int cells = matches.Count;

            int cols = cells / rows;
            int index = 0;

            ITable table = new Table(rows, cols);

            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    table.SetRawValue(r, c, matches[index++].Groups[1].Value);

            return table;
        }
    }
}
