using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CSC449_SeatReservationSystem.Entity
{
    
    public class Address
    {
        [Key]
        [Required]
        public int AddressId { get; set; }

        [Required]
        [StringLength(20)]
        public string Number { get; set; }
        
        [Required]
        [StringLength(200)]
        public string StreetName { get; set; }

        [Required]
        [StringLength(200)]
        public string City { get; set; }

        [Required]
        [StringLength(10)]
        public string State { get; set; }

        [Required]
        [StringLength(20)]
        public string Zip { get; set;  }

        private Address() { }
    }
}
