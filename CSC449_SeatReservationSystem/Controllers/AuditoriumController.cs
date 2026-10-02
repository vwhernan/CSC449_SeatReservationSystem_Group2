using CSC449_SeatReservationSystem.Entity;
using CSC449_SeatReservationSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static CSC449_SeatReservationSystem.Entity.Auditorium;
using static CSC449_SeatReservationSystem.Entity.MovieTheater;

namespace CSC449_SeatReservationSystem.Controllers
{
    public class AuditoriumController : Controller
    {

        private IRepository<Auditorium, AuditoriumModel> _repo;
        public AuditoriumController(IRepository<Auditorium, AuditoriumModel> repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> Details(int id)
        {
            var auditorium = await _repo.GetByIdAsync(id);

            return View(auditorium);
        }


        [HttpGet]
        public IActionResult Create(int theaterId)
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
                var result = await _repo.CreateAsync(form).ConfigureAwait(false);
                return RedirectToAction("Details", "MovieTheater", new {theaterId});
            }
            
            ViewBag.TheaterId = theaterId;
            return View(form);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int auditoriumId)
        {
            var auditorium = _repo.GetByIdAsync(auditoriumId)
                .ToModelAsync()
                .ConfigureAwait(false);

            return View(auditorium);
        
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AuditoriumModel form, int auditoriumId)
        {
            if (ModelState.IsValid)
            {
                var result = await _repo.UpdateAsync(form, auditoriumId).ConfigureAwait(false);
                if (result == true)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewBag.AuditoriumId = auditoriumId;
            return View(form);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int auditoriumId)
        {
            var model = await _repo.GetByIdAsync(auditoriumId).ConfigureAwait(false);
            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DoDelete(int auditoriumId)
        {
            if (ModelState.IsValid)
            {
                var result = await _repo.DeleteAsync(auditoriumId).ConfigureAwait(false);
                if (result == true)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewBag.AuditoriumId = auditoriumId;
            return RedirectToAction(nameof(Details), auditoriumId);
        }



    }
}
