using CSC449_SeatReservationSystem.Entity;
using CSC449_SeatReservationSystem.Interfaces;
using CSC449_SeatReservationSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using static CSC449_SeatReservationSystem.Entity.Auditorium;
using static CSC449_SeatReservationSystem.Entity.Seat;

namespace CSC449_SeatReservationSystem.Controllers
{
    
    public class SeatController : Controller
    {
        private readonly IRepository<Seat, SeatModel> _repo;

        public SeatController(IRepository<Seat, SeatModel> repo)
        {
            _repo = repo;
        }


        public async Task<IActionResult> Index()
        {
            
            var seats = await _repo.GetAllAsync().ConfigureAwait(false);

            return View(seats);
        }

        public async Task<IActionResult> Details(int id)
        {
            var seat = await _repo.GetByIdAsync(id);

            return View(seat);
        }

        [HttpGet]
        public IActionResult Create(int auditoriumId)
        {
            var model = new SeatModel
            {
                AuditoriumId = auditoriumId
            };

            ViewBag.AuditoriumId = auditoriumId;

            return View(model);
 
        }

        [HttpPost]
        public async Task<IActionResult> Create(SeatModel form, int auditoriumId)
        {
            if (ModelState.IsValid)
            {
                var result = await _repo.CreateAsync(form).ConfigureAwait(false);
                return RedirectToAction("Details", "Auditorium", new { id = auditoriumId });
            }

            ViewBag.AuditoriumId = auditoriumId;
            return View(form);
        }

    }
}
