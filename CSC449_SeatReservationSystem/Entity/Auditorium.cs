using System.ComponentModel.DataAnnotations;

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
        }

        public void AddSeat(Seat seat)
        {
            if (seat == null){throw new ArgumentNullException(nameof(seat));}
            Seats.Add(seat);
        }

        public void AddShowtime(Showtime showtime)
        {
            if (showtime == null){throw new ArgumentNullException(nameof(showtime));}
            else
            {
                Showtimes!.Add(showtime);
            }
        }

        public class AuditoriumModel
        {
            [Required]
            [StringLength(100)]
            public string Name { get; set; } = null!;

        }
    }
}