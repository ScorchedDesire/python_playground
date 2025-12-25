namespace Excel.Shared.DTO
{
    public class SessionResponse
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
