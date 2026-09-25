using CSC449_SeatReservationSystem.Entity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Emit;

namespace CSC449_SeatReservationSystem
{
    public class MovieMagicDbContext : DbContext
    {
        
        public DbSet<MovieTheater> MovieTheaters { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Auditorium> TheaterAuditoriums { get; set; }
        public DbSet<Seat> Seats { get; set; }

        public MovieMagicDbContext(DbContextOptions options) : base(options)
        {
        }

        protected MovieMagicDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Scans and applies all IEntityTypeConfiguration classes in this assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
