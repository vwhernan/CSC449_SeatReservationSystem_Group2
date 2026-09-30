using CSC449_SeatReservationSystem.Entity;
using CSC449_SeatReservationSystem.Interfaces;
using CSC449_SeatReservationSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using static CSC449_SeatReservationSystem.Entity.MovieTheater;

namespace CSC449_SeatReservationSystem.Controllers
{
    public class MovieTheaterController : Controller
    {
        private IRepository<MovieTheater, MovieTheaterModel> _db;
        public MovieTheaterController(IRepository<MovieTheater, MovieTheaterModel> db)
        {
            _db= db;
        }
        public async Task<IActionResult> Index()
        {
            var theaters = await _db.GetAllAsync().ConfigureAwait(false);
            return View(theaters);
        }


        public async Task<IActionResult> Details(int theaterId)
        {
            var theater = await _db.GetByIdAsync(theaterId).ConfigureAwait(false);
            ViewBag.TheaterId = theaterId;
            return View(theater);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new MovieTheaterModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(MovieTheaterModel form)
        {
            if (ModelState.IsValid)
            {
                var result = await _db.CreateAsync(form).ConfigureAwait(false);
                if (result == true)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(form);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int theaterId)
        {
            var model = await _db.GetByIdAsync(theaterId)
                .ToModelAsync()
                .ConfigureAwait(false);
            ViewBag.TheaterId = theaterId;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MovieTheaterModel form, int theaterId)
        {
            if (ModelState.IsValid)
            {
                var result = await _db.UpdateAsync(form, theaterId).ConfigureAwait(false);
                if (result == true)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewBag.TheaterId = theaterId;
            return View(form);
        }



        [HttpGet]
        public async Task<IActionResult> Delete(int theaterId)
        {
            var model = await _db.GetByIdAsync(theaterId).ConfigureAwait(false);
            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DoDelete(int theaterId)
        {
            if (ModelState.IsValid)
            {
                var result = await _db.DeleteAsync(theaterId).ConfigureAwait(false);
                if (result == true)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewBag.TheaterId = theaterId;
            return RedirectToAction(nameof(Details), theaterId);
        }

    }
}
