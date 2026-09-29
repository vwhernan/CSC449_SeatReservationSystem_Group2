using CSC449_SeatReservationSystem.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using static CSC449_SeatReservationSystem.Entity.Movie;

namespace MovieMagicTests
{
    public class MovieTests
    {
        [Fact]
        public void CreateMovie()
        {
            //Arrange
            MovieModel movieModel = new MovieModel
            {
                Name = "Interstellar",
                Rating = Ratings.PG13,
                Genres = Genres.SciFi,
                MovieLengthMinutes = 169
            };

            //Act
            Movie movie = new Movie(movieModel);

            //Assert
            Assert.Equal(movieModel.Name, movie.Name);
            Assert.Equal(movieModel.Rating, movie.Rating);
            Assert.Equal(movieModel.Genres, movie.Genres);
            Assert.Equal(movieModel.MovieLengthMinutes, movie.MovieLengthMinutes);

        }

        [Fact]
        public void CreateMovieWithEmptyName()
        {
            //Arrange
            MovieModel movieModel = new MovieModel
            {
                Name = "",
                Rating = Ratings.PG13,
                Genres = Genres.SciFi,
                MovieLengthMinutes = 169
            };

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Movie(movieModel));
        }
    }
}
