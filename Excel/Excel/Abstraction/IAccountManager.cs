using Excel.Shared.DTO;

namespace Excel.Core.Abstraction
{
    public interface IAccountManager
    {
        Task<bool> Register(RegisterRequest req);
        Task<bool> Login(LoginRequest req);
        Task<bool> AutoLogin();
        Task<UserInfo?> GetAccountInfo();
        void Logout();
    }
}
