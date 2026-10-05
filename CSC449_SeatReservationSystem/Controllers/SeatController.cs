using CSC449_SeatReservationSystem.Entity;
using CSC449_SeatReservationSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static CSC449_SeatReservationSystem.Entity.Auditorium;
using static CSC449_SeatReservationSystem.Entity.Seat;

namespace CSC449_SeatReservationSystem.Controllers
{
    public class SeatController : Controller
    {
        private IRepository<Seat, SeatModel> _repo;

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

        [HttpGet]
        public async Task<IActionResult> Edit(int seatId)
        {
            var seat = await _repo.GetByIdAsync(seatId)
                .ToModelAsync()
                .ConfigureAwait(false);

            ViewBag.AuditoriumId = seat.AuditoriumId;
            ViewBag.SeatId = seatId;
            return View(seat);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SeatModel form, int seatId)
        {
            if (ModelState.IsValid)
            {
                var result = await _repo.UpdateAsync(form, seatId).ConfigureAwait(false);
                if (result == true)
                {
                    return RedirectToAction(nameof(Details), new { id = seatId });
                }
            }

            ViewBag.SeatId = seatId;
            ViewBag.AuditoriumId = form.AuditoriumId;
            return View(form);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int seatId)
        {
            var model = await _repo.GetByIdAsync(seatId).ConfigureAwait(false);
            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DoDelete(int seatId, int auditoriumId)
        {
            if (ModelState.IsValid)
            {
                var result = await _repo.DeleteAsync(seatId).ConfigureAwait(false);
                if (result == true)
                {
                    return RedirectToAction("Details", "Auditorium", new { id = auditoriumId });
                }
            }

            ViewBag.SeatId = seatId;
            return RedirectToAction(nameof(Details), new { id = seatId });
        }
    }
}