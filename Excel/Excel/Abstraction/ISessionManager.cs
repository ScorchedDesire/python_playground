namespace Excel.Core.Abstraction
{
    public interface ISessionManager
    {
        void SaveToken(string token);
        string? LoadToken();
        void ClearToken();
        bool HasToken { get; }
    }
}
