using CSC449_SeatReservationSystem.Entity;
using CSC449_SeatReservationSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static CSC449_SeatReservationSystem.Entity.Auditorium;

namespace CSC449_SeatReservationSystem.Repositories
{
    public class AuditoriumRepository : IRepository<Auditorium, AuditoriumModel>
    {
        private readonly MovieMagicDbContext _db;

        public AuditoriumRepository(MovieMagicDbContext db)
        {
            _db = db;
        }
        public async Task<bool> CreateAsync(AuditoriumModel form)
        {
            ArgumentNullException.ThrowIfNull(form);

            
            var theater = await _db.MovieTheaters
                .Include(t => t.Auditoriums)
                .FirstOrDefaultAsync(t => t.TheaterId == form.TheaterId);

            if (theater == null)
            {
                return false; 
            }
            var auditorium = new Auditorium(form);
           
            theater.AddAuditorium(auditorium);

            int rowsAffected = await _db.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
           var auditorium = await _db.TheaterAuditoriums
                .SingleAsync(t => t.Id == id)
                .ConfigureAwait(false);
            if (auditorium == null)
            {
                return false;
            }

            _db.TheaterAuditoriums.Remove(auditorium);
            int rowsAffected = await _db.SaveChangesAsync();
            return rowsAffected > 0;

        }

        public async Task<ICollection<Auditorium>> GetAllAsync()
        {
            return await _db.TheaterAuditoriums
                .Include(a => a.Seats)
                .Include(a => a.Showtimes)
                .ThenInclude(s => s.Movie)
                .ToListAsync();
        }

        public async Task<Auditorium> GetByIdAsync(int id)
        {
            return await _db.TheaterAuditoriums
                .Include(a => a.Seats)
                .Include(a => a.Showtimes)
                .ThenInclude(s => s.Movie)
                .SingleAsync(a => a.Id == id);
        }

        public async Task<bool> UpdateAsync(AuditoriumModel form, int id)
        {
            ArgumentNullException.ThrowIfNull(form);

            var auditorium = await _db.TheaterAuditoriums.FindAsync(id);
            if (auditorium == null)
            {
                return false;
            }

            auditorium.UpdateAuditoriumInfo(form);
            int rowsAffected = await _db.SaveChangesAsync();
            return rowsAffected > 0;
        }
    }
}
