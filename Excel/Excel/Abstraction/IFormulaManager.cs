namespace Excel.Core.Abstraction
{
    public interface IFormulaManager : ICellObserver
    {
        object? EvaluateCell(int row, int col);
    }
}
