using System;
using System.Collections.Generic;
using System.Text;
using CSC449_SeatReservationSystem.Entity;
using Xunit;

namespace MovieMagicTests
{
    public class ShowtimeTests
    {
        [Fact]
        public void CreateShowtime_WithValid_Data_CreateShowtime()
        {
            //Arrange
            var movie = new Movie(new Movie.MovieModel
            {
                Name = "Addams Family Values",
                Rating = Ratings.PG13,
                Genres = Genres.Comedy,
                MovieLengthMinutes = 94
            });

            var model = new Showtime.ShowtimeModel
            { 
                StartTime = DateTime.Now.AddHours(2),
                TicketPrice = 12.50,
                Movie = movie,
                AuditoriumId = 1
            };

            //Act
            var showtime = new Showtime(model);

            //Assert
            Assert.Equal(model.StartTime, showtime.StartTime);
            Assert.Equal(12.50, showtime.TicketPrice, 2);
            Assert.Equal(movie, showtime.Movie);
            Assert.Equal(1, showtime.AuditoriumId);
        }

        [Fact]
        public void UpdateTicketPrice_WithNegativePrice_ThrowsException()
        {
            //Arrange
            var movie = new Movie(new Movie.MovieModel
            {
                Name = "Addams Family Values",
                Rating = Ratings.PG13,
                Genres = Genres.Comedy,
                MovieLengthMinutes = 94
            });

            var model = new Showtime.ShowtimeModel
            {
                StartTime = DateTime.Now.AddHours(2),
                TicketPrice = 12.50,
                Movie = movie,
                AuditoriumId = 1
            };

            var Showtime = new Showtime(model);

            //Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Showtime.UpdateTicketPrice(-5.00));
        }   
    }
}
