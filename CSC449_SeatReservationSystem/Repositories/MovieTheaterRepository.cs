using CSC449_SeatReservationSystem.Entity;
using CSC449_SeatReservationSystem.Interfaces;
using Microsoft.EntityFrameworkCore;
using static CSC449_SeatReservationSystem.Entity.Address;
using static CSC449_SeatReservationSystem.Entity.MovieTheater;

namespace CSC449_SeatReservationSystem.Repositories
{
    public class MovieTheaterRepository : IMovieTheaterRepository
    {
        private readonly MovieMagicDbContext _db;

        public MovieTheaterRepository(MovieMagicDbContext db)
        {
            _db = db;
        }
        public async Task<bool> CreateAsync(MovieTheaterModel form)
        {
            if (form == null || string.IsNullOrWhiteSpace(form.Name) || form.Address == null)
            {
                return false;
            }

            var movieTheater = new MovieTheater(form);

            await _db.MovieTheaters.AddAsync(movieTheater).ConfigureAwait(false);
            int rowsAffected = await _db.SaveChangesAsync().ConfigureAwait(false);

            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var movieTheater = await _db.MovieTheaters
                .Include(m => m.Address)
                .Include(m => m.Auditoriums)
                .SingleOrDefaultAsync(m => m.TheaterId == id)
                .ConfigureAwait(false);

            if (movieTheater == null)
            {
                return false;
            }
            
            //Auditoriums is the class on movie theater, TheaterAuditoriums is in the DB
            if (movieTheater.Auditoriums != null && movieTheater.Auditoriums.Any())
            {
                _db.TheaterAuditoriums.RemoveRange(movieTheater.Auditoriums);
            }

            _db.MovieTheaters.Remove(movieTheater);

            int rowsAffected = await _db.SaveChangesAsync().ConfigureAwait(false);

            return rowsAffected > 0;
        }

        public async Task<ICollection<MovieTheater>> GetAllAsync()
        {
            return await _db.MovieTheaters
                .Include(t => t.Address)
                .Include(t => t.Auditoriums)
                .ToArrayAsync()
                .ConfigureAwait(false);
        }

        public async Task<MovieTheater> GetByIdAsync(int id)
        {
            return await _db.MovieTheaters
                .Include(t => t.Address)
                .Include(t => t.Auditoriums)
                .Include(t => t.NowPlaying)
                .SingleAsync(t => t.TheaterId == id)
                .ConfigureAwait(false);
                
        }

        public async Task<bool> UpdateAsync(MovieTheaterModel form, int id)
        {
            if (form == null)
            {
                return false;
            }

            var existingTheater = await _db.MovieTheaters
            .Include(t => t.Address)
            .FirstOrDefaultAsync(t => t.TheaterId == id);

            if (existingTheater == null)
            {
                return false;
            }

            existingTheater.UpdateTheaterInfo(form);

            var rowsAffected = await _db.SaveChangesAsync().ConfigureAwait(false);

            return rowsAffected > 0;
        }

        public async Task<bool> UpdateNowPlayingMoviesAsync(int theaterId, List<int> selectedMovieIds)
        {
            var theater = await _db.MovieTheaters
                .Include(t => t.NowPlaying)
                .FirstOrDefaultAsync(t => t.TheaterId == theaterId)
                .ConfigureAwait(false);

            if (theater == null) return false;

            // Fetch all movies matching selected IDs
            var selectedMovies = await _db.Movies
                .Where(m => selectedMovieIds.Contains(m.Id))
                .ToListAsync()
                .ConfigureAwait(false);

            // Sync NowPlaying list
            theater.NowPlaying.Clear();
            foreach (var movie in selectedMovies)
            {
                theater.NowPlaying.Add(movie);
            }

            int rowsAffected = await _db.SaveChangesAsync().ConfigureAwait(false);
            return rowsAffected > 0;
        }

    }
}
