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
        public DbSet<Seat> Seats {  get; set; }
        public DbSet<SeatType> SeatTypes { get; set; }
        public DbSet<Terminal> Terminals { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Salt> Salts {  get; set; } 
        public FlyCompanyDBContext(DbContextOptions<FlyCompanyDBContext> options) :base (options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airport>(entity =>
            {
                entity.HasOne(e => e.Country).WithMany(e=>e.Airports).OnDelete(DeleteBehavior.NoAction);
                entity.HasMany(e => e.Terminals).WithOne(e=>e.Airport).OnDelete(DeleteBehavior.NoAction);
                entity.HasMany(e => e.Gates).WithOne(e=>e.Airport).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Flight>(entity =>
            {
                entity.HasOne(e => e.To).WithMany(e => e.In).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(e => e.From).WithMany(e => e.Out).OnDelete(DeleteBehavior.NoAction);
            });
            modelBuilder.Entity<Seat>(entity =>
            {
                entity.HasOne(e => e.Flight).WithMany(e => e.Seats).OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}
