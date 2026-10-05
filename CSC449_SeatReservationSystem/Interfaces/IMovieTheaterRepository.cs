using CSC449_SeatReservationSystem.Entity;
using static CSC449_SeatReservationSystem.Entity.MovieTheater;

namespace CSC449_SeatReservationSystem.Interfaces
{
    public interface IMovieTheaterRepository : IRepository<MovieTheater, MovieTheaterModel>
    {
        Task<bool> UpdateNowPlayingMoviesAsync(int theaterId, List<int> selectedMovieIds);
    }
}