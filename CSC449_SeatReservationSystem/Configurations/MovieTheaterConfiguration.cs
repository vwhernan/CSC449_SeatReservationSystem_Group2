using CSC449_SeatReservationSystem.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSC449_SeatReservationSystem.Configurations
{
    public class MovieTheaterConfiguration : IEntityTypeConfiguration<MovieTheater>
    {
        public void Configure(EntityTypeBuilder<MovieTheater> builder)
        {
            
            // One-to-One with Address (Cascade Delete)
            builder.HasOne(mt => mt.Address)
                .WithOne()
                .HasForeignKey<Address>(a => a.TheaterId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-Many with Auditoriums (Cascade Delete)
            builder.HasMany(mt => mt.Auditoriums)
                .WithOne()
                .HasForeignKey(a => a.TheaterId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
