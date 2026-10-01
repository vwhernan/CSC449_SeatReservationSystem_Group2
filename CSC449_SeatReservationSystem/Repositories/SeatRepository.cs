using CSC449_SeatReservationSystem.Entity;
using CSC449_SeatReservationSystem.Interfaces;
using Microsoft.EntityFrameworkCore;
using static CSC449_SeatReservationSystem.Entity.Seat;

namespace CSC449_SeatReservationSystem.Repositories
{
    public class SeatRepository : IRepository<Seat, SeatModel>
    {
        private readonly MovieMagicDbContext _db;

        public SeatRepository(MovieMagicDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateAsync(SeatModel form)
        {
            ArgumentNullException.ThrowIfNull(form);

            var auditorium = await _db.TheaterAuditoriums
                .Include(a => a.Seats)
                .FirstOrDefaultAsync(a => a.Id == form.AuditoriumId);

            if (auditorium == null)
            {
                return false;
            }

            var seat = new Seat(form);
            auditorium.AddSeat(seat);

            int rowsAffected = await _db.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var seat = await _db.Seats.FindAsync(id);
            if (seat == null)
            {
                return false;
            }

            _db.Seats.Remove(seat);
            int rowsAffected = await _db.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public async Task<ICollection<Seat>> GetAllAsync()
        {
            return await _db.Seats.ToListAsync();
        }

        public async Task<Seat?> GetByIdAsync(int id)
        {
            return await _db.Seats.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(SeatModel form, int id)
        {
            ArgumentNullException.ThrowIfNull(form);

            var seat = await _db.Seats.FindAsync(id);
            if (seat == null)
            {
                return false;
            }

            seat.UpdateSeatInfo(form);
            int rowsAffected = await _db.SaveChangesAsync();
            return rowsAffected > 0;
        }
    }
}
