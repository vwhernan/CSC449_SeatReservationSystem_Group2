using CSC449_SeatReservationSystem.Entity;
using CSC449_SeatReservationSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static CSC449_SeatReservationSystem.Entity.Showtime;

namespace CSC449_SeatReservationSystem.Controllers
{
    public class ShowtimeController : Controller
    {
        private readonly IRepository<Showtime, ShowtimeModel> _repo;

        public ShowtimeController(IRepository<Showtime, ShowtimeModel> repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Create(int auditoriumId)
        {
            var model = new ShowtimeModel
            {
                AuditoriumId = auditoriumId,

                
            };

            return View();
        }
    }
}
