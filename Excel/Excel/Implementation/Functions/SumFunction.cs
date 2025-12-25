using Excel.Core.Abstraction;

namespace Excel.Implementation.Classes.Functions
{
    public class SumFunction : IFunction
    {
        public string Name => "SUM";
        public object? Execute(IEnumerable<object?> args)
        {
            double total = 0;

            foreach (var arg in args)
            {
                // Skip invalid or empty cells
                if (arg == null)
                    continue;

                // Add only numeric values
                if (double.TryParse(arg.ToString(), out double value))
                    total += value;
            }

            return total;
        }
    }
}