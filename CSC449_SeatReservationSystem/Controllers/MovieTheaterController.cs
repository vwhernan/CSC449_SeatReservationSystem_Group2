using Microsoft.AspNetCore.Mvc;

namespace CSC449_SeatReservationSystem.Controllers
{
    public class MovieTheaterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
