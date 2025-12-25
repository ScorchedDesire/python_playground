using Excel.Core.Abstraction;

namespace Excel.Core.Implementation.Classes
{
    public class SessionManager : ISessionManager
    {
        // This is our session manager
        // it handles saving, loading, and clearing session tokens
        private const string SessionFile = "session.dat";
        public bool HasToken => File.Exists(SessionFile);

        public void SaveToken(string token)
        {
            File.WriteAllText(SessionFile, token);
        }

        public string? LoadToken()
        {
            return HasToken ? File.ReadAllText(SessionFile) : null;
        }

        public void ClearToken()
        {
            if (File.Exists(SessionFile))
                File.Delete(SessionFile);
        }
    }
}