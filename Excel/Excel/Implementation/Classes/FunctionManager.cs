using Excel.Core.Abstraction;

namespace Excel.Core.Implementation.Classes
{
    public class FunctionManager : IFunctionManager
    {
        private readonly Dictionary<string, IFunction> _functions =
            new Dictionary<string, IFunction>();
        // Automatically register all IFunction implementations
        // This means that any class implementing IFunction will be automatically registered, no hardcoding needed
        public void AutoRegisterAll()
        {
            var functionType = typeof(IFunction);

            // Get all types implementing IFunction
            // This uses reflection to find all classes that implement the IFunction interface
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => functionType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            foreach (var type in types)
            {
                var instance = (IFunction)Activator.CreateInstance(type)!;
                Register(instance);
            }
        }

        // Register a single function
        public void Register(IFunction function)
        {
            _functions[function.Name] = function;
        }

        // Try to get a function by name
        public bool TryGet(string name, out IFunction? function)
        {
            return _functions.TryGetValue(name, out function);
        }
        public IEnumerable<string> GetFunctionNames()
        {
            return _functions.Keys;
        }

        public IFunction Get(string name)
        {
            return _functions[name];
        }
    }
}
