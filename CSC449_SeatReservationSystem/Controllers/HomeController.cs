using CSC449_SeatReservationSystem.Entity;
using CSC449_SeatReservationSystem.Interfaces;
using CSC449_SeatReservationSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static CSC449_SeatReservationSystem.Entity.MovieTheater;

namespace CSC449_SeatReservationSystem.Controllers
{
    public class HomeController : Controller
    {
        public IMovieTheaterRepository _movieTheaterRepo { get; }

        public HomeController(IMovieTheaterRepository movieTheaterRepo)
        {
            _movieTheaterRepo = movieTheaterRepo;
        }
        public async Task<IActionResult> Index()
        {
            var theaters = await _movieTheaterRepo.GetAllAsync();


            return View(theaters);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
