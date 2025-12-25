namespace Excel.Core.Abstraction
{
    public interface ITable : ICellSubject
    {
        int RowCount { get; }
        int ColumnCount { get; }
        object? GetRawValue(int row, int col);
        object? GetDisplayValue(int row, int col);
        void SetRawValue(int row, int col, object? value);
        void SetDisplayValue(int row, int col, object? value);

        event Action<int, int>? CellChanged;
    }
}
