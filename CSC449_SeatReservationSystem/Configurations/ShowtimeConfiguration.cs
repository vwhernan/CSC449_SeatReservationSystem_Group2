using CSC449_SeatReservationSystem.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSC449_SeatReservationSystem.Configurations
{
    public class ShowtimeConfiguration : IEntityTypeConfiguration<Showtime>
    {
        public void Configure(EntityTypeBuilder<Showtime> builder)
        {

            // Configure Navigation to Movie if Movie has its own primary key
            builder.HasOne(st => st.Movie)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
