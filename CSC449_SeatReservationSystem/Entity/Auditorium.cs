using System.ComponentModel.DataAnnotations;
using static CSC449_SeatReservationSystem.Entity.Address;
using static CSC449_SeatReservationSystem.Entity.Auditorium;
using static CSC449_SeatReservationSystem.Entity.MovieTheater;

namespace CSC449_SeatReservationSystem.Entity
{
    public class Auditorium
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        [StringLength(100)]
        public string Name { get; private set; } = null!;

        public virtual int TheaterId { get; private set; }

        public virtual ICollection<Seat>? Seats { get; private set; } = new List<Seat>();
        public virtual ICollection<Showtime>? Showtimes { get; private set; } = new List<Showtime>();

        private Auditorium() { }

        public Auditorium(AuditoriumModel form)
        {
            if (form.Name == null) { throw new NullReferenceException(nameof(form.Name)); }
            Name = form.Name;
            TheaterId = form.TheaterId;
        }

        public void UpdateAuditoriumInfo(AuditoriumModel form)
        {
            ArgumentNullException.ThrowIfNull(form);
            Name = form.Name;
            
        }

        public void AddSeat(Seat seat)
        {

            if (seat == null){throw new ArgumentNullException(nameof(seat));}
            else
            {
                Seats.Add(seat);
            }
        }

        public void AddShowtime(Showtime showtime)
        {
            ArgumentNullException.ThrowIfNull(showtime);


            // Check if any existing showtime overlaps with the incoming showtime
            bool hasOverlap = Showtimes.Any(s =>
                showtime.StartTime < s.EndTime && showtime.EndTime > s.StartTime);

            if (hasOverlap)
            {
                throw new InvalidOperationException("This showtime overlaps with an existing showtime in this auditorium.");
            }

            Showtimes.Add(showtime);
        }

        public class AuditoriumModel
        {
            [Required]
            [StringLength(100)]
            public string Name { get; set; } = null!;

            public int TheaterId { get; set; }

        }
    }

    public static class AuditoriumExtensions
    {
        public static AuditoriumModel ToModel(this Auditorium auditorium)
        {
            if (auditorium == null) return null;

            return new AuditoriumModel
            {
                Name = auditorium.Name,
                TheaterId = auditorium.TheaterId,
            };
        }

        public static async Task<AuditoriumModel> ToModelAsync(this Task<Auditorium> result)
        => await result.ContinueWith(r => r.Result.ToModel());
    }
}