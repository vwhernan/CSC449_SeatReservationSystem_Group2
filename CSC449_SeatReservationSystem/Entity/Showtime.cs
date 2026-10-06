using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CSC449_SeatReservationSystem.Entity.Auditorium;
using static CSC449_SeatReservationSystem.Entity.Showtime;

namespace CSC449_SeatReservationSystem.Entity
{
    public class Showtime
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        public DateTime StartTime { get; private set; }


        public DateTime EndTime => Movie != null
        ? StartTime.AddMinutes(Movie.MovieLengthMinutes)
        : StartTime;

        [Required]
        public double TicketPrice { get; private set; }

        [Required]
        public virtual Movie Movie { get; private set; } = null!;

        [Required]
        public virtual int AuditoriumId { get; private set; }

        private Showtime() { }

        public Showtime(ShowtimeModel form)
        {
            if (form == null){throw new ArgumentNullException(nameof(form));}

            if (form.Movie == null){throw new ArgumentNullException(nameof(form.Movie), "A showtime must have an associated movie.");}

            if (form.TicketPrice < 0){throw new ArgumentOutOfRangeException(nameof(form.TicketPrice), "Ticket price cannot be negative.");}

            if (form.AuditoriumId <= 0){throw new ArgumentOutOfRangeException(nameof(form.AuditoriumId), "A valid AuditoriumId must be provided.");}

            StartTime = form.StartTime;
            TicketPrice = form.TicketPrice;
            Movie = form.Movie;
            AuditoriumId = form.AuditoriumId;
        }

        public void UpdateStartTime(DateTime newStartTime)
        {
            StartTime = newStartTime;
        }

        public void UpdateTicketPrice(double newPrice)
        {
            if (newPrice < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(newPrice), "Ticket price cannot be negative.");
            }

            TicketPrice = newPrice;
        }

        public class ShowtimeModel
        {
            [Required]
            public DateTime StartTime { get; set; }

            [Required]
            [Range(0, 1000)]
            public double TicketPrice { get; set; }

            [Required]
            public Movie Movie { get; set; } = null!;

            [Required]
            public int AuditoriumId { get; set; }
        }

    }

    public static class ShowTimeExtensions
    {
        public static ShowtimeModel ToModel(this Showtime showtime)
        {
            if (showtime == null) return null;

            return new ShowtimeModel
            {
                StartTime = showtime.StartTime,
                TicketPrice = showtime.TicketPrice,
                Movie = showtime.Movie,
                AuditoriumId = showtime.AuditoriumId
            };
        }

        public static async Task<ShowtimeModel> ToModelAsync(this Task<Showtime> result)
        => await result.ContinueWith(r => r.Result.ToModel());
    }
}