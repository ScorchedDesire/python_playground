namespace Excel.Core.Abstraction
{
    public interface ICellObserver
    {
        void OnCellChanged(int row, int col);
    }
}
