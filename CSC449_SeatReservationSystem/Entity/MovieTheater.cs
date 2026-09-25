using System.ComponentModel.DataAnnotations;

namespace CSC449_SeatReservationSystem.Entity
{
    public class MovieTheater
    {
        [Key]
        public int TheaterId { get; private set; }

        [Required]
        public Address Address { get; private set; } = null!;

        [Required]
        [StringLength(100)]
        public string Name { get; private set; } = null!;

        public virtual ICollection<Auditorium>? Auditoriums { get; private set; } = new List<Auditorium>();

        private MovieTheater(){ }
        public MovieTheater(MovieTheaterModel form)
        {
            if (form.Address == null) { throw new NullReferenceException(nameof(form.Address));}
            if (form.Name == null) { throw new NullReferenceException(nameof(form.Name)); }

            Name = form.Name;
            Address = form.Address;
            
        }

        public void AddAuditorium(Auditorium auditorium)
        {
            if (auditorium == null) { throw new NullReferenceException(nameof(auditorium)); }
            else
            {
                Auditoriums!.Add(auditorium);
            }
        }

        public class MovieTheaterModel()
        {
            [Required]
            public Address Address { get;  set; } 

            [Required]
            [StringLength(100)]
            public string Name { get; set; } 

        }
    }
}
