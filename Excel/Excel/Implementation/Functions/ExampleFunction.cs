using Excel.Core.Abstraction;

namespace Excel.Core.Implementation.Functions
{
    public class ExampleFunction : IFunction
    {
        // Implement your function HERE.
        public string Name => "ABC"; // Insert any function name here
        public object? Execute(IEnumerable<object?> args)
        {
            var argList = args.ToList();

            argList.Sort();

            return argList[0];
        }
    }
}
