using Microsoft.EntityFrameworkCore;
using Models;

namespace DBLayer
{
    public class FlyCompanyDBContext : DbContext
    {
        
        public DbSet<Salt> Salts {  get; set; }  
        public FlyCompanyDBContext(DbContextOptions<FlyCompanyDBContext> options) :base (options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
