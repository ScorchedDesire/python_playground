namespace Excel.Server.Models
{
    public enum LicenseType
    {
        Trial,
        Standard,
        Lifetime,
        Void
    }
    public class License
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public User User { get; set; }
        public LicenseType Type { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public DateTime? ExpirationDate { get; set; } 
    }
}
