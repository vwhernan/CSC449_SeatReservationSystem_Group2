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

            //Get theater so we can add the auditorium
            var theater = await _db.MovieTheaters
                .Include(t => t.Auditoriums)
                .FirstOrDefaultAsync(t => t.TheaterId == form.TheaterId);

            if (theater == null)
            {
                return false; // Theater not found
            }

            var auditorium = new Auditorium(form);

            // Add to the MovieTheater's navigation collection
            theater.AddAuditorium(auditorium);

            //Save changes to the database
            int rowsAffected = await _db.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Auditorium>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Auditorium> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(AuditoriumModel form, int id)
        {
            throw new NotImplementedException();
        }
    }
}
