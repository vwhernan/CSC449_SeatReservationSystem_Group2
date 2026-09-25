using System.ComponentModel.DataAnnotations;

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

        public void UpdateSeatType(SeatType newType)
        {
            SeatType = newType;
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
        Normal,
        Ada_Accessable,
        Out_Of_Order
    }
}