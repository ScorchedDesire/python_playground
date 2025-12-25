using Excel.Core.Abstraction;
using System.Globalization;

namespace Excel.Implementation.Classes.Functions
{
    public class AvgFunction : IFunction
    {
        public string Name => "AVG";

        public object? Execute(IEnumerable<object?> args)
        {
            double sum = 0;
            int count = 0;

            foreach (var arg in args)
            {
                if (arg == null || string.IsNullOrWhiteSpace(arg.ToString()))
                    continue;

                if (double.TryParse(arg.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
                {
                    sum += value;
                    count++;
                }
            }
            if (count == 0) return "#ERR";

            return sum / count;
        }
    }
}