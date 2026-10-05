using System.ComponentModel.DataAnnotations;
using static CSC449_SeatReservationSystem.Entity.Address;
using static CSC449_SeatReservationSystem.Entity.MovieTheater;
using static CSC449_SeatReservationSystem.Entity.Seat;

namespace CSC449_SeatReservationSystem.Entity
{
    public class Seat
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        [StringLength(10)]
        public string Row { get; private set; } = null!;
        
        [Required]
        public int SeatNumber { get; private set; }
        
        [Required]
        public SeatType SeatType { get; private set; }

        [Required]
        public virtual int AuditoriumId { get; private set; }

        private Seat() { }

        public Seat(SeatModel form)
        {
            if (form == null){throw new ArgumentNullException(nameof(form));}

            if (string.IsNullOrWhiteSpace(form.Row)){throw new ArgumentNullException(nameof(form.Row), "Seat row cannot be null or empty.");}

            if (form.SeatNumber <= 0){throw new ArgumentOutOfRangeException(nameof(form.SeatNumber), "Seat number must be greater than zero.");}

            if (form.AuditoriumId <= 0){throw new ArgumentOutOfRangeException(nameof(form.AuditoriumId), "A valid AuditoriumId must be provided.");}

            Row = form.Row;
            SeatNumber = form.SeatNumber;
            SeatType = form.SeatType;
            AuditoriumId = form.AuditoriumId;
        }

        public void UpdateSeatInfo(SeatModel form)
        {
            ArgumentNullException.ThrowIfNull(form);
            ArgumentNullException.ThrowIfNullOrEmpty(form.Row);
            if (form.SeatNumber <= 0) { throw new ArgumentOutOfRangeException(nameof(form.SeatNumber), "Seat number must be greater than zero."); }
            if (form.AuditoriumId <= 0) { throw new ArgumentOutOfRangeException(nameof(form.AuditoriumId), "A valid AuditoriumId must be provided."); }

            Row = form.Row;
            SeatNumber = form.SeatNumber;
            SeatType = form.SeatType;
        }

        public class SeatModel
        {
            [Required]
            [StringLength(10)]
            public string Row { get; set; } = null!;

            [Required]
            public int SeatNumber { get; set; }

            [Required]
            public SeatType SeatType { get; set; } = SeatType.Normal;

            [Required]
            public int AuditoriumId { get; set; }
        }
    }

    public enum SeatType
    {
        [Display(Name = "Standard")]
        Normal,

        [Display(Name = "ADA Accessible")]
        Ada_Accessable,

        [Display(Name = "Out of Order")]
        Out_Of_Order
    }

    public static class SeatExtensions
    {
        public static SeatModel ToModel(this Seat seat)
        {
            if (seat == null) return null;

            return new SeatModel
            {
                Row = seat.Row,
                SeatNumber = seat.SeatNumber,
                SeatType=seat.SeatType,
                AuditoriumId = seat.AuditoriumId,
            };
        }

        public static async Task<SeatModel> ToModelAsync(this Task<Seat> result)
        => await result.ContinueWith(r => r.Result.ToModel());
    }

}