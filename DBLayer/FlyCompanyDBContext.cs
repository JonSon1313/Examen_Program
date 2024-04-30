using Microsoft.EntityFrameworkCore;
using Models;

namespace DBLayer
{
    public class FlyCompanyDBContext : DbContext
    {
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Gate>Gates { get; set; }
        public DbSet<Seat> Seast {  get; set; }
        public DbSet<SeatType> SeatTypes { get; set; }
        public DbSet<Terminal> Terminals { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Salt> Salts {  get; set; }  
        public FlyCompanyDBContext(DbContextOptions<FlyCompanyDBContext> options) :base (options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
