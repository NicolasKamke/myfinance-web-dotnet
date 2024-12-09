using Microsoft.EntityFrameworkCore;
using myfinance_web_dotnet.Domain.Models.Entities;

namespace myfinance_web_dotnet.Infrastructure
{
    public class MyFinanceDbContext: DbContext
    {
        public DbSet<AccountPlan> AccountPlan { get; set; }
        public DbSet<AccountTransaction> AccountTransaction { get; set; }
        public DbSet<AccountType> AccountType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = @"Server=host.docker.internal,1433;Database=master;User Id=SA;Password=Password_123#;TrustServerCertificate=True";
            optionsBuilder.UseSqlServer(connectionString);
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
