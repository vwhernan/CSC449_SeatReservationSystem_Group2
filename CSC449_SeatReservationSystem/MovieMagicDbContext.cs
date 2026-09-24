using CSC449_SeatReservationSystem.Entity;
using Microsoft.EntityFrameworkCore;

namespace CSC449_SeatReservationSystem
{
    public class MovieMagicDbContext : DbContext
    {
        
        public DbSet<MovieTheater> MovieTheaters { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public MovieMagicDbContext(DbContextOptions options) : base(options)
        {
        }

        protected MovieMagicDbContext()
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Address>()
                .HasKey(a=> a.AddressId);

            builder.Entity<MovieTheater>(entity =>
            {
                entity.HasKey(m => m.TheaterId);
                entity.HasOne(m => m.Address);
            });
                
           

        }
    }
}
