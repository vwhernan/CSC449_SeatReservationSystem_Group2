using CSC449_SeatReservationSystem.Entity;
using CSC449_SeatReservationSystem.Interfaces;
using CSC449_SeatReservationSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static CSC449_SeatReservationSystem.Entity.Movie;

namespace CSC449_SeatReservationSystem.Controllers
{
    public class MovieController : Controller
    {
        private readonly IRepository<Movie, MovieModel> _db;

        public MovieController(IRepository<Movie, MovieModel> db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _db.GetAllAsync().ConfigureAwait(false);
            return View(movies);
        }

        public async Task<IActionResult> Details(int movieId)
        {
            var movie = await _db.GetByIdAsync(movieId).ConfigureAwait(false);
            if (movie == null) return NotFound();

            ViewBag.MovieId = movieId;
            return View(movie);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new MovieModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieModel form)
        {
            if (ModelState.IsValid)
            {
                var result = await _db.CreateAsync(form).ConfigureAwait(false);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, "Unable to save movie.");
            }

            return View(form);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int movieId)
        {
            var model = await _db.GetByIdAsync(movieId)
                .ToModelAsync()
                .ConfigureAwait(false);

            ViewBag.MovieId = movieId;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MovieModel form, int movieId)
        {
            if (ModelState.IsValid)
            {
                var result = await _db.UpdateAsync(form, movieId).ConfigureAwait(false);
                if (result)
                {
                    return RedirectToAction(nameof(Details), new { movieId });
                }

                ModelState.AddModelError(string.Empty, "Unable to make changes to movie.");
            }

            ViewBag.MovieId = movieId;
            return View(form);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int movieId)
        {
            var movie = await _db.GetByIdAsync(movieId).ConfigureAwait(false);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DoDelete(int movieId)
        {
            var result = await _db.DeleteAsync(movieId).ConfigureAwait(false);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Details), new { movieId });
        }


    }
}
