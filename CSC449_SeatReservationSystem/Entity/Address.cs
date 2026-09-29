using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;

namespace CSC449_SeatReservationSystem.Entity
{

    public class Address
    {
        [Key]
        public int Id { get; private set; }


        [Required]
        [StringLength(200)]
        public string Street { get; private set; } = null!;

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

            if (string.IsNullOrWhiteSpace(form.Street)){throw new ArgumentNullException(nameof(form.Street), "Street name cannot be null or empty.");}

            if (string.IsNullOrWhiteSpace(form.City)){throw new ArgumentNullException(nameof(form.City), "City cannot be null or empty.");}

            if (string.IsNullOrWhiteSpace(form.State)){ throw new ArgumentNullException(nameof(form.State), "State cannot be null or empty.");}

            if (string.IsNullOrWhiteSpace(form.Zip)){throw new ArgumentNullException(nameof(form.Zip), "Zip code cannot be null or empty.");}

            Street = form.Street;
            City = form.City;
            State = form.State;
            Zip = form.Zip;
            
        }

        public void UpdateAddressInfo(AddressModel model)
        {
            ArgumentNullException.ThrowIfNull(model);

            Street = model.Street;
            City = model.City;
            State = model.State;
            Zip = model.Zip;
            
        }

        public class AddressModel
        {

            [Required]
            [StringLength(200)]
            public string Street { get; set; } = null!;

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
