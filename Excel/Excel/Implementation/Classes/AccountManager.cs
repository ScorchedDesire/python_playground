using Excel.Core.Abstraction;
using Excel.Server.Data;
using Excel.Server.Services;
using Excel.Shared.DTO;
using Microsoft.EntityFrameworkCore;

namespace Excel.Core.Implementation.Classes
{
    // This is our Account manager
    // it handles registration, login, auto-login, logout, and fetching account info
    public class AccountManager : IAccountManager
    {
        private readonly ISessionManager _sessionManager;
        private readonly AuthService _authService;

        public AccountManager(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
            _authService = new AuthService();
        }

        public async Task<bool> Register(RegisterRequest req)
        {
            return await _authService.Register(req);
        }

        public async Task<bool> Login(LoginRequest req)
        {
            var session = await _authService.Login(req);

            if (session == null)
                return false;

            _sessionManager.SaveToken(session.Token);
            return true;
        }

        public async Task<bool> AutoLogin()
        {
            var token = _sessionManager.LoadToken();
            if (token == null)
                return false;

            var user = await _authService.ValidateToken(token);
            return user != null;
        }

        public void Logout()
        {
            _sessionManager.ClearToken();
        }

        public async Task<UserInfo?> GetAccountInfo()
        {
            var token = _sessionManager.LoadToken();
            if (token == null)
                return null;

            var user = await _authService.ValidateToken(token);
            if (user == null)
                return null;

            using var db = DbContextFactory.Create();

            var license = await db.Licenses
                .SingleOrDefaultAsync(l => l.ID == user.LicenseID);

            if (license == null)
                return null;

            return new UserInfo
            {
                Username = user.Username,
                Email = user.Email,
                License = (LicenseType)license.Type,
                ExpirationDate = license.ExpirationDate
            };
        }
    }
}
