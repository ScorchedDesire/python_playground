namespace Excel.Core.Abstraction
{
    public interface IFunctionManager
    {
        void AutoRegisterAll();
        void Register(IFunction function);
        bool TryGet(string name, out IFunction? function);
        IEnumerable<string> GetFunctionNames();
        IFunction Get(string name);
    }
}
