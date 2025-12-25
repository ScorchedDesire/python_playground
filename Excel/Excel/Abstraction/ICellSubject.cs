namespace Excel.Core.Abstraction
{
    public interface ICellSubject
    {
        void AddObserver(ICellObserver observer);
        void RemoveObserver(ICellObserver observer);
    }
}
