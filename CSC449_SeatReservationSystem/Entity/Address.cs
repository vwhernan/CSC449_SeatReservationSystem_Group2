using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CSC449_SeatReservationSystem.Entity
{

    public class Address
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        [StringLength(20)]
        public string Number { get; private set; } = null!;

        [Required]
        [StringLength(200)]
        public string StreetName { get; private set; } = null!;

        [Required]
        [StringLength(200)]
        public string City { get; private set; } = null!;

        [Required]
        [StringLength(10)]
        public string State { get; private set; } = null!;

        [Required]
        [StringLength(20)]
        public string Zip { get; private set; } = null!;

        public virtual int TheaterId { get; private set; }

        private Address() { }

        public Address(AddressModel form)
        {
            if (form == null){throw new ArgumentNullException(nameof(form));}

            if (string.IsNullOrWhiteSpace(form.Number)){throw new ArgumentNullException(nameof(form.Number), "Street number cannot be null or empty.");}

            if (string.IsNullOrWhiteSpace(form.StreetName)){throw new ArgumentNullException(nameof(form.StreetName), "Street name cannot be null or empty.");}

            if (string.IsNullOrWhiteSpace(form.City)){throw new ArgumentNullException(nameof(form.City), "City cannot be null or empty.");}

            if (string.IsNullOrWhiteSpace(form.State)){ throw new ArgumentNullException(nameof(form.State), "State cannot be null or empty.");}

            if (string.IsNullOrWhiteSpace(form.Zip)){throw new ArgumentNullException(nameof(form.Zip), "Zip code cannot be null or empty.");}

            Number = form.Number;
            StreetName = form.StreetName;
            City = form.City;
            State = form.State;
            Zip = form.Zip;
            
        }

        public class AddressModel
        {
            [Required]
            [StringLength(20)]
            public string Number { get; set; } = null!;

            [Required]
            [StringLength(200)]
            public string StreetName { get; set; } = null!;

            [Required]
            [StringLength(200)]
            public string City { get; set; } = null!;

            [Required]
            [StringLength(10)]
            public string State { get; set; } = null!;

            [Required]
            [StringLength(20)]
            public string Zip { get; set; } = null!;
        }
    }
}
