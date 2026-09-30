using CSC449_SeatReservationSystem.Entity;
using CSC449_SeatReservationSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static CSC449_SeatReservationSystem.Entity.Auditorium;
using static CSC449_SeatReservationSystem.Entity.MovieTheater;

namespace CSC449_SeatReservationSystem.Controllers
{
    public class AuditoriumController : Controller
    {

        private IRepository<Auditorium, AuditoriumModel> _db;
        public AuditoriumController(IRepository<Auditorium, AuditoriumModel> db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Create(int theaterId)
        {
            var model = new AuditoriumModel
            {
                TheaterId = theaterId
            };

            ViewBag.TheaterId = theaterId;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuditoriumModel form, int theaterId)
        {
            if (ModelState.IsValid)
            {
                var result = await _db.CreateAsync(form).ConfigureAwait(false);
                return RedirectToAction("Details", "MovieTheater", new {theaterId});
            }
            
            ViewBag.TheaterId = theaterId;
            return View(form);
        }
    }
}
