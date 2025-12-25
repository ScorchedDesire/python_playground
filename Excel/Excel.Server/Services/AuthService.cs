using Excel.Server.Data;
using Excel.Shared.DTO;
using Excel.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Excel.Server.Services
{
    public class AuthService
    {
        private readonly string _pepper = "A0FD03F2F92049";

        public async Task<bool> Register(RegisterRequest request)
        {
            using var db = DbContextFactory.Create();

            // Username or email in use?
            if (await db.Users.AnyAsync(u => u.Username == request.Username))
                return false;

            if (await db.Users.AnyAsync(u => u.Email == request.Email))
                return false;

            // Generate salt
            string salt = Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
            string combined = request.Password + salt + _pepper;
            string hash = BCrypt.Net.BCrypt.HashPassword(combined);

            // Create user
            var user = new Excel.Server.Models.User
            {
                ID = Guid.NewGuid(),
                Username = request.Username,
                Email = request.Email,
                PasswordHash = hash,
                Salt = salt
            };

            // Create trial license (30 days)
            var license = new License
            {
                ID = Guid.NewGuid(),
                UserID = user.ID,
                Type = Models.LicenseType.Trial,
                CreationDate = DateTime.UtcNow,
                ExpirationDate = DateTime.UtcNow.AddDays(30)
            };

            user.LicenseID = license.ID;

            Guid NewId = Guid.NewGuid();
            var session = new Session
            {
                Id = NewId,
                UserId = user.ID,
                Token = TokenGenerator.GenerateToken(NewId),
                ExpiresAt = DateTime.UtcNow.AddDays(14)
            };

            db.Users.Add(user);
            db.Licenses.Add(license);
            db.Sessions.Add(session);
            await db.SaveChangesAsync();

            return true;
        }

        public async Task<SessionResponse?> Login(LoginRequest request)
        {
            using var db = DbContextFactory.Create();

            var user = await db.Users.SingleOrDefaultAsync(u =>
                u.Username == request.Username ||
                u.Email == request.Username);

            if (user == null)
                return null;

            string combined = request.Password + user.Salt + _pepper;

            if (!BCrypt.Net.BCrypt.Verify(combined, user.PasswordHash))
                return null;

            Guid NewId = Guid.NewGuid();
            var session = new Session
            {
                Id = NewId,
                UserId = user.ID,
                Token = TokenGenerator.GenerateToken(NewId),
                ExpiresAt = DateTime.UtcNow.AddDays(14)
            };

            db.Sessions.Add(session);
            await db.SaveChangesAsync();

            return new SessionResponse
            {
                Token = session.Token,
                ExpiresAt = session.ExpiresAt
            };
        }

        public async Task<User?> ValidateToken(string token)
        {
            using var db = DbContextFactory.Create();

            var session = await db.Sessions
                .SingleOrDefaultAsync(s => s.Token == token);

            if (session == null)
                return null;

            // Check expiration
            if (session.ExpiresAt < DateTime.UtcNow)
                return null;

            // Load the user
            return await db.Users.SingleOrDefaultAsync(u => u.ID == session.UserId);
        }
    }
}
