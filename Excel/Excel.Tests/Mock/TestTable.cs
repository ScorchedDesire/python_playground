using Excel.Core.Abstraction;

namespace Excel.Tests.Mock
{
    public class TestTable : ITable, ICellSubject
    {
        private readonly object?[,] _raw;
        private readonly object?[,] _display;

        private readonly List<ICellObserver> _observers = new();

        public int RowCount { get; }
        public int ColumnCount { get; }

        public event Action<int, int>? CellChanged;

        public TestTable(int rows, int cols)
        {
            RowCount = rows;
            ColumnCount = cols;
            _raw = new object?[rows, cols];
            _display = new object?[rows, cols];
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

        public object? GetRawValue(int r, int c) => _raw[r, c];

        public object? GetDisplayValue(int r, int c) => _display[r, c];

        public void SetRawValue(int r, int c, object? value)
        {
            _raw[r, c] = value;

            // Fire event for observers (FormulaManager)
            CellChanged?.Invoke(r, c);

            foreach (var o in _observers)
                o.OnCellChanged(r, c);
        }

        public void SetDisplayValue(int r, int c, object? value)
        {
            _display[r, c] = value;
        }
    }
}