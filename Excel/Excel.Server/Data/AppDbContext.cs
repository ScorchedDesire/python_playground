using Excel.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Excel.Server.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<License> Licenses { get; set; }
        public DbSet<Session> Sessions { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=ExcelDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.License)
                .WithOne(l => l.User)
                .HasForeignKey<License>(l => l.UserID);

            modelBuilder.Entity<Session>()
                .HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId);
        }
    }
}
