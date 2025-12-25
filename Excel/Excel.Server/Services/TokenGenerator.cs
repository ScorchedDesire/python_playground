using System.Security.Cryptography;

namespace Excel.Server.Services
{
    public class TokenGenerator
    {
        public static string GenerateToken(Guid Id)
        {
            byte[] randomBytes = RandomNumberGenerator.GetBytes(512);
            randomBytes.Concat(Id.ToByteArray());
            string randomPart = Convert.ToBase64String(randomBytes);
            return $"{randomPart}";
        }
    }
}
