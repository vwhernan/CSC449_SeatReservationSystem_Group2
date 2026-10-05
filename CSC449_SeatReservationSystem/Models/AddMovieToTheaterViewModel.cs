using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CSC449_SeatReservationSystem.Models
{
    public class AddMovieToTheaterViewModel
    {
        public int TheaterId { get; set; }
        public string TheaterName { get; set; }
        public List<MovieSelectionViewModel> Movies { get; set; } = new();
    }

    public class MovieSelectionViewModel
    {
        public int MovieId { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}