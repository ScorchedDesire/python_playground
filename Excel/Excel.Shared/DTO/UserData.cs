namespace Excel.Shared.DTO
{
    public enum LicenseType
    {
        Trial,
        Standard,
        Lifetime,
        Void
    }
    public class UserInfo
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public LicenseType License { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
