using CSC449_SeatReservationSystem.Entity;
using CSC449_SeatReservationSystem.Interfaces;
using Microsoft.EntityFrameworkCore;
using static CSC449_SeatReservationSystem.Entity.Showtime;

namespace CSC449_SeatReservationSystem.Repositories
{
    public class ShowtimeRepository : IRepository<Showtime, ShowtimeModel>
    {

        private readonly MovieMagicDbContext _db;

        public ShowtimeRepository(MovieMagicDbContext db)
        {
            _db = db;
        }
        public async Task<bool> CreateAsync(ShowtimeModel form)
        {
            if (form == null) throw new ArgumentNullException(nameof(form));

            var auditorium = await _db.TheaterAuditoriums
                .Include(a => a.Showtimes)
                .FirstOrDefaultAsync(a => a.Id == form.AuditoriumId)
                .ConfigureAwait(false);

            if (auditorium == null)
            {
                throw new KeyNotFoundException($"Auditorium with ID {form.AuditoriumId} was not found.");
            }

            // Inform EF that the Movie already exists in the database so it doesn't try to insert it
            if (form.Movie != null)
            {
                _db.Movies.Attach(form.Movie);
            }

            var showtime = new Showtime(form);

            auditorium.AddShowtime(showtime);
            await _db.Showtimes.AddAsync(showtime).ConfigureAwait(false);

            var result = await _db.SaveChangesAsync().ConfigureAwait(false);

            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var showtime = await _db.Showtimes
                .FirstOrDefaultAsync(s => s.Id == id)
                .ConfigureAwait(false);

            if (showtime == null)
            {
                return false;
            }

            _db.Showtimes.Remove(showtime);
            var result = await _db.SaveChangesAsync().ConfigureAwait(false);

            return result > 0;
        }

        public async Task<ICollection<Showtime>> GetAllAsync()
        {
            return await _db.Showtimes
                .Include(s => s.Movie)
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<Showtime> GetByIdAsync(int id)
        {
            var showtime = await _db.Showtimes
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(s => s.Id == id)
                .ConfigureAwait(false);

            if (showtime == null)
            {
                throw new KeyNotFoundException($"Showtime with ID {id} was not found.");
            }

            return showtime;
        }

        public async Task<bool> UpdateAsync(ShowtimeModel form, int id)
        {
            if (form == null) throw new ArgumentNullException(nameof(form));

            var existingShowtime = await _db.Showtimes
                .FirstOrDefaultAsync(s => s.Id == id)
                .ConfigureAwait(false);

            if (existingShowtime == null)
            {
                return false;
            }

            // Update parameters using entity methods
            existingShowtime.UpdateStartTime(form.StartTime);
            existingShowtime.UpdateTicketPrice(form.TicketPrice);

            _db.Showtimes.Update(existingShowtime);
            var result = await _db.SaveChangesAsync().ConfigureAwait(false);

            return result > 0;
        }
    }
}
