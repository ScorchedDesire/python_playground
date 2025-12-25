using Excel.Core.Abstraction;

namespace Excel.Core.Implementation.Entities
{
    public class Table : ITable
    {
        private readonly object?[,] _raw;
        private readonly object?[,] _display;

        private readonly List<ICellObserver> _observers = new();

        public event Action<int, int>? CellChanged;

        public int RowCount => _raw.GetLength(0);
        public int ColumnCount => _raw.GetLength(1);

        public Table(int rows, int cols)
        {
            _raw = new object?[rows, cols];
            _display = new object?[rows, cols];
        }

        public object? GetRawValue(int row, int col)
        {
            return _raw[row, col];
        }

        public object? GetDisplayValue(int row, int col)
        {
            return _display[row, col];
        }

        public void SetDisplayValue(int row, int col, object? value)
        {
            _display[row, col] = value;
        }

        public void SetRawValue(int row, int col, object? value)
        {
            _raw[row, col] = value;

            CellChanged?.Invoke(row, col);

            foreach (var obs in _observers)
                obs.OnCellChanged(row, col);
        }

        public void AddObserver(ICellObserver observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
        }

        public void RemoveObserver(ICellObserver observer)
        {
            _observers.Remove(observer);
        }
    }
}

