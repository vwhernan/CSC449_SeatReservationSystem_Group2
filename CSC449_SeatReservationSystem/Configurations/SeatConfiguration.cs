using CSC449_SeatReservationSystem.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSC449_SeatReservationSystem.Configurations
{
    public class SeatConfiguration : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> builder)
        {
          
            // Store Enum as string for better readability in database
            builder.Property(s => s.SeatType)
                .HasConversion<string>()
                .HasMaxLength(30);
        }
    }
}

