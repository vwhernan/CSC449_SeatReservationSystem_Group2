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
        [Display(Name="Name")]
        public string Name { get; private set; } = null!;

        public virtual ICollection<Auditorium>? Auditoriums { get; private set; } = new List<Auditorium>();

        private MovieTheater(){ }
        public MovieTheater(MovieTheaterModel form)
        {
            if (form.Address == null) { throw new NullReferenceException(nameof(form.Address));}
            if (form.Name == null) { throw new NullReferenceException(nameof(form.Name)); }

            Name = form.Name;
            Address = new Address(form.Address);
            
        }


        public void UpdateTheaterInfo(MovieTheaterModel form)
        {
            ArgumentNullException.ThrowIfNull(form);
            ArgumentNullException.ThrowIfNull(form.Address);
            ArgumentNullException.ThrowIfNull(form.Name);

            Name = form.Name;
            Address.UpdateAddressInfo(form.Address);
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
            [StringLength(100)]
            [Display(Name = "Name")]
            public string Name { get; set; } = null!;

            [Required]
            public Address.AddressModel Address { get; set; } = new Address.AddressModel();

        }
    }
}
