using CSC449_SeatReservationSystem.Entity;
using CSC449_SeatReservationSystem.Interfaces;
using Microsoft.EntityFrameworkCore;
using static CSC449_SeatReservationSystem.Entity.Movie;

namespace CSC449_SeatReservationSystem.Repositories
{
    public class MovieRepository : IRepository<Movie, MovieModel>
    {
        private readonly MovieMagicDbContext _db;

        public MovieRepository(MovieMagicDbContext db)
        {
            _db = db;
        }

        public async Task<ICollection<Movie>> GetAllAsync()
        {
            return await _db.Movies
                .AsNoTracking()
                .OrderBy(m => m.Name)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<Movie?> GetByIdAsync(int id)
        {
            return await _db.Movies
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id)
                .ConfigureAwait(false);
        }

        public async Task<bool> CreateAsync(MovieModel model)
        {
            var movie = new Movie(model);

            _db.Movies.Add(movie);
            return await _db.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        public async Task<bool> UpdateAsync(MovieModel model, int id)
        {
            if (model is null)
            {
                return false;
            }

            var rowsAffected = await _db.Movies
                .Where(m => m.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(m => m.Name, model.Name)
                    .SetProperty(m => m.Rating, model.Rating)
                    .SetProperty(m => m.Genres, model.Genres)
                    .SetProperty(m => m.MovieLengthMinutes, model.MovieLengthMinutes))
                .ConfigureAwait(false);

            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var movie = await _db.Movies.FindAsync(id).ConfigureAwait(false);
            if (movie is null)
            {
                return false;
            }

            _db.Movies.Remove(movie);
            return await _db.SaveChangesAsync().ConfigureAwait(false) > 0;
        }
    }

    public static class MovieExtensions
    {
        public static MovieModel ToModel(this Movie movie) => new MovieModel
        {
            Name = movie.Name,
            Rating = movie.Rating,
            Genres = movie.Genres,
            MovieLengthMinutes = movie.MovieLengthMinutes
        };

        public static async Task<MovieModel?> ToModelAsync(this Task<Movie> result)
        {
            var movie = await result.ConfigureAwait(false);
            return movie?.ToModel();
        }
    }
}
