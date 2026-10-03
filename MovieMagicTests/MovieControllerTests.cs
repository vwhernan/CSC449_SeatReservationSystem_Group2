using CSC449_SeatReservationSystem.Controllers;
using CSC449_SeatReservationSystem.Entity;
using CSC449_SeatReservationSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static CSC449_SeatReservationSystem.Entity.Movie;

namespace MovieMagicTests
{
    public class MovieControllerTests
    {
        //Test repository to simulate the database operations for movies
        private class TestMovieRepository : IRepository<Movie, MovieModel>
        {
            public List<Movie> Movies { get; } = new();

            public Task<bool> CreateAsync(MovieModel form)
            {
                Movies.Add(new Movie(form));
                return Task.FromResult(true);
            }

            public Task<ICollection<Movie>> GetAllAsync()
                => Task.FromResult<ICollection<Movie>>(Movies);

            public Task<Movie> GetByIdAsync(int id)
                => Task.FromResult(Movies.FirstOrDefault(m => m.Id == id)!);

            public Task<bool> UpdateAsync(MovieModel form, int id) => Task.FromResult(false);

            public Task<bool> DeleteAsync(int id) => Task.FromResult(false);
        }

        // As a theater manager, I need a way to add new movies and showtimes.[/+]
        [Fact]
        public async Task AddNewMovie_ValidMovie_SavesAndRedirectsToIndex()
        {
            var repo = new TestMovieRepository();
            var controller = new MovieController(repo);
            var model = new MovieModel
            {
                Name = "Alice In Wonderland",
                Rating = Ratings.PG,
                Genres = Genres.Fantasy,
                MovieLengthMinutes = 108
            };

            // Act
            var result = await controller.Create(model);

            //Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
             Assert.Equal("Index", redirect.ActionName);
            Assert.Single(repo.Movies);
            Assert.Equal("Alice In Wonderland", repo.Movies[0].Name);

        }


        // As a customer, I need to be able to view the movies available so I can select a movie.
        [Fact]
        public async Task ViewMovies_ReturnsAllAvailableMovies()
        {
            //Arrange
            var repo = new TestMovieRepository();
            repo.Movies.Add(new Movie(new MovieModel
            {
                Name = "Alice In Wonderland", Rating = Ratings.PG,
                Genres = Genres.Fantasy, MovieLengthMinutes = 108
            }));

            repo.Movies.Add(new Movie(new MovieModel
            {
                Name = "Project Hail Mary", Rating = Ratings.PG13,
                Genres = Genres.SciFi, MovieLengthMinutes = 156
            }));

            var controller = new MovieController(repo);

            //Act
            var result = await controller.Index();

            //Assert
            var view = Assert.IsType<ViewResult>(result);
            var movies = Assert.IsAssignableFrom<ICollection<Movie>>(view.Model);
            Assert.Equal(2, movies.Count);
            Assert.Contains(movies, m => m.Name == "Project Hail Mary");
        }
    }
}