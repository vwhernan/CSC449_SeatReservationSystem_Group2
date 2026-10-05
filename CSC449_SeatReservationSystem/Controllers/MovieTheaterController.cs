using CSC449_SeatReservationSystem.Entity;
using CSC449_SeatReservationSystem.Interfaces;
using CSC449_SeatReservationSystem.Models;
using CSC449_SeatReservationSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static CSC449_SeatReservationSystem.Entity.Movie;
using static CSC449_SeatReservationSystem.Entity.MovieTheater;

namespace CSC449_SeatReservationSystem.Controllers
{
    public class MovieTheaterController : Controller
    {
        private readonly IMovieTheaterRepository _movieTheaterRepo;
        private readonly IRepository<Movie, MovieModel> _movieRepo;

        public MovieTheaterController(IMovieTheaterRepository movieTheaterRepo, IRepository<Movie, MovieModel> movieRepo)
        {
            _movieTheaterRepo = movieTheaterRepo;
            _movieRepo = movieRepo;
        }


        public async Task<IActionResult> Index()
        {
            var theaters = await _movieTheaterRepo.GetAllAsync().ConfigureAwait(false);
            return View(theaters);
        }


        public async Task<IActionResult> Details(int theaterId)
        {
            var theater = await _movieTheaterRepo.GetByIdAsync(theaterId).ConfigureAwait(false);
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
                var result = await _movieTheaterRepo.CreateAsync(form).ConfigureAwait(false);
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
            var model = await _movieTheaterRepo.GetByIdAsync(theaterId)
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
                var result = await _movieTheaterRepo.UpdateAsync(form, theaterId).ConfigureAwait(false);
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
            var model = await _movieTheaterRepo.GetByIdAsync(theaterId).ConfigureAwait(false);
            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DoDelete(int theaterId)
        {
            if (ModelState.IsValid)
            {
                var result = await _movieTheaterRepo.DeleteAsync(theaterId).ConfigureAwait(false);
                if (result == true)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewBag.TheaterId = theaterId;
            return RedirectToAction(nameof(Details), theaterId);
        }

        [HttpGet]
        public async Task<IActionResult> ManageMovies(int theaterId)
        {
            var theater = await _movieTheaterRepo.GetByIdAsync(theaterId);
            if (theater == null) return NotFound();

            var allMovies = await _movieRepo.GetAllAsync();
            var nowPlayingIds = theater.NowPlaying.Select(m => m.Id).ToHashSet();

            var model = new AddMovieToTheaterViewModel
            {
                TheaterId = theater.TheaterId,
                TheaterName = theater.Name,
                Movies = allMovies.Select(m => new MovieSelectionViewModel
                {
                    MovieId = m.Id,
                    Name = m.Name,
                    IsSelected = nowPlayingIds.Contains(m.Id)
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageMovies(AddMovieToTheaterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var selectedMovieIds = model.Movies
                    .Where(m => m.IsSelected)
                    .Select(m => m.MovieId)
                    .ToList();

                var success = await _movieTheaterRepo.UpdateNowPlayingMoviesAsync(model.TheaterId, selectedMovieIds);
                if (success)
                {
                    return RedirectToAction(nameof(Details), new { theaterId = model.TheaterId });
                }

                ModelState.AddModelError(string.Empty, "Unable to update movies for theater.");
            }

            return View(model);
        }

    }
}
