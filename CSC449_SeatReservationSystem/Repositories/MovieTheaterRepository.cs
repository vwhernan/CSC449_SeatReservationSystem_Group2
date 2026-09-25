using CSC449_SeatReservationSystem.Entity;
using CSC449_SeatReservationSystem.Interfaces;
using Microsoft.EntityFrameworkCore;
using static CSC449_SeatReservationSystem.Entity.MovieTheater;

namespace CSC449_SeatReservationSystem.Repositories
{
    public class MovieTheaterRepository : IRepository<MovieTheater, MovieTheaterModel>
    {
        private readonly MovieMagicDbContext _db;

        public MovieTheaterRepository(MovieMagicDbContext db)
        {
            _db = db;
        }
        public async Task<bool> CreateAsync(MovieTheaterModel form)
        {
            if(form.Name == null || form.Address == null)
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

        public Task<ICollection<MovieTheater>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<MovieTheater> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(MovieTheater form)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(MovieTheaterModel form)
        {
            throw new NotImplementedException();
        }
    }
}
