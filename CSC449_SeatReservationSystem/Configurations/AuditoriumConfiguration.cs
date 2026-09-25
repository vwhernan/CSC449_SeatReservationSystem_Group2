using CSC449_SeatReservationSystem.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSC449_SeatReservationSystem.Configurations
{
    public class AuditoriumConfiguration : IEntityTypeConfiguration<Auditorium>
    {
        public void Configure(EntityTypeBuilder<Auditorium> builder)
        {
            
            // One-to-Many with Seats (Cascade Delete)
            builder.HasMany(a => a.Seats)
                .WithOne()
                .HasForeignKey(s => s.AuditoriumId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-Many with Showtimes (Cascade Delete)
            builder.HasMany(a => a.Showtimes)
                .WithOne()
                .HasForeignKey(st => st.AuditoriumId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
