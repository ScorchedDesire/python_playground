using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Excel.Server.Data
{
    public class DbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public static AppDbContext Create()
        {
            return new AppDbContext();
        }

        public AppDbContext CreateDbContext(string[] args)
        {
            return new AppDbContext();
        }
    }
}
