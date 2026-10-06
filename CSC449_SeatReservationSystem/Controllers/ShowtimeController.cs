using CSC449_SeatReservationSystem.Entity;
using CSC449_SeatReservationSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static CSC449_SeatReservationSystem.Entity.Movie;
using static CSC449_SeatReservationSystem.Entity.Showtime;

namespace CSC449_SeatReservationSystem.Controllers
{
    public class ShowtimeController : Controller
    {
        private readonly IRepository<Showtime, ShowtimeModel> _showtimeRepo;
        private readonly IRepository<Movie, MovieModel> _movieRepo;

        public ShowtimeController(
            IRepository<Showtime, ShowtimeModel> showtimeRepo,
            IRepository<Movie, MovieModel> movieRepo)
        {
            _showtimeRepo = showtimeRepo;
            _movieRepo = movieRepo;
        }

        public async Task<IActionResult> Index()
        {
            var showtimes = await _showtimeRepo.GetAllAsync().ConfigureAwait(false);
            return View(showtimes);
        }

        public async Task<IActionResult> Details(int id)
        {
            var showtime = await _showtimeRepo.GetByIdAsync(id).ConfigureAwait(false);
            return View(showtime);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int auditoriumId)
        {
            var model = new ShowtimeModel
            {
                AuditoriumId = auditoriumId,
                StartTime = DateTime.Now
            };

            await PopulateMoviesDropDownListAsync().ConfigureAwait(false);
            ViewBag.AuditoriumId = auditoriumId;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ShowtimeModel form, int auditoriumId, int selectedMovieId)
        {
            // Get Movie to Add
            if (selectedMovieId > 0)
            {
                form.Movie = await _movieRepo.GetByIdAsync(selectedMovieId).ConfigureAwait(false);
                ModelState.Remove("Movie"); 
            }

            if (ModelState.IsValid)
            {
                var result = await _showtimeRepo.CreateAsync(form).ConfigureAwait(false);
                if (result)
                {
                    return RedirectToAction("Details", "Auditorium", new { id = auditoriumId });
                }
            }

            await PopulateMoviesDropDownListAsync(selectedMovieId).ConfigureAwait(false);
            ViewBag.AuditoriumId = auditoriumId;
            return View(form);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int showtimeId)
        {
            var showtime = await _showtimeRepo.GetByIdAsync(showtimeId).ConfigureAwait(false);
            if (showtime == null) return NotFound();

            var model = new ShowtimeModel
            {
                StartTime = showtime.StartTime,
                TicketPrice = showtime.TicketPrice,
                Movie = showtime.Movie,
                AuditoriumId = showtime.AuditoriumId
            };

            await PopulateMoviesDropDownListAsync(showtime.Movie?.Id).ConfigureAwait(false);
            ViewBag.AuditoriumId = showtime.AuditoriumId;
            ViewBag.ShowtimeId = showtimeId;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ShowtimeModel form, int showtimeId, int selectedMovieId)
        {
            if (selectedMovieId > 0)
            {
                form.Movie = await _movieRepo.GetByIdAsync(selectedMovieId).ConfigureAwait(false);
                ModelState.Remove("Movie");
            }

            if (ModelState.IsValid)
            {
                var result = await _showtimeRepo.UpdateAsync(form, showtimeId).ConfigureAwait(false);
                if (result)
                {
                    return RedirectToAction(nameof(Details), new { id = showtimeId });
                }
            }

            await PopulateMoviesDropDownListAsync(selectedMovieId).ConfigureAwait(false);
            ViewBag.ShowtimeId = showtimeId;
            ViewBag.AuditoriumId = form.AuditoriumId;
            return View(form);
        }

        
        private async Task PopulateMoviesDropDownListAsync(object? selectedMovie = null)
        {
            var movies = await _movieRepo.GetAllAsync().ConfigureAwait(false);
            ViewBag.Movies = new SelectList(movies, "Id", "Name", selectedMovie);
        }
    }
}