using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DBLayer
{
    public class FlyCompanyDBContextBuilder : IDesignTimeDbContextFactory<FlyCompanyDBContext>
    {
        public FlyCompanyDBContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            var options = new DbContextOptionsBuilder<FlyCompanyDBContext>()
                .UseSqlServer(config.GetConnectionString("SqlClient")).Options;

            return new FlyCompanyDBContext(options);
        }
    }
}
