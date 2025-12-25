namespace Excel.Core.Abstraction
{
    using System.Collections.Generic;

    public interface IFunction
    {
        string Name { get; }
        object? Execute(IEnumerable<object?> args);
    }
}
