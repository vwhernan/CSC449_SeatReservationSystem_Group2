using CSC449_SeatReservationSystem.Entity;
using Microsoft.Identity.Client;
using static CSC449_SeatReservationSystem.Entity.Address;
using static CSC449_SeatReservationSystem.Entity.MovieTheater;

namespace MovieMagicTests
{
    public class MovieTheaterTests
    {

        [Fact]
        public void CreateAddressFromModel()
        {
            //Create a address Model
            AddressModel addressModel = new AddressModel
            {

                Street = "123 Main St",
                City = "Stardew Valley",
                State = "California",
                Zip = "12345"

            };

            Address address = new Address(addressModel);

            Assert.Equal(address.Street, addressModel.Street);
            Assert.Equal(address.City, addressModel.City);
            Assert.Equal(address.State, addressModel.State);
            Assert.Equal(address.Zip, addressModel.Zip);

        }


        [Fact]
        public void CreateMovieTheaterFromModel()
        {
            //Create a address Model
            AddressModel address = new AddressModel {
                
                Street = "123 Main St",
                City = "Stardew Valley",
                State= "California",
                Zip="12345"

            };

            //Create a theater Model, and add address
            MovieTheaterModel model = new MovieTheaterModel
            {
                Name = "Star Cinema",
                Address = new Address(address)
            };

            //create movie theater entity
            MovieTheater theater = new MovieTheater(model);

            //Assert Model Data binded to entity correctly
            Assert.Equal(model.Name, theater.Name);
            Assert.Equal(model.Address.Street, theater.Address.Street);
            Assert.Equal(model.Address.City, theater.Address.City);
            Assert.Equal(model.Address.State, theater.Address.State);
            Assert.Equal(model.Address.Zip, theater.Address.Zip);

            // Assert Auditoriums is empty
            Assert.NotNull(theater.Auditoriums);
            Assert.Empty(theater.Auditoriums);

        }

        [Fact]
        public void AddAuditoriumToMovieTheater()
        {
            throw new NotImplementedException();
        }

        
    }
}
