using Excel.Core.Abstraction;

namespace Excel.Implementation.Classes.Functions
{
    public class CountFunction : IFunction
    {
        public string Name => "COUNT";
        public object? Execute(IEnumerable<object?> args)
        {
            int count = 0;

            foreach (var arg in args)
            {
                // Parses only cells with numeric values
                if (double.TryParse(arg?.ToString(), out _))
                    count++;
            }

            return count;
        }
    }
}