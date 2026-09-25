using System.ComponentModel.DataAnnotations;

namespace CSC449_SeatReservationSystem.Entity
{
    public class Movie
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        [StringLength(200)]
        public string Name { get; private set; } = null!;

        public Ratings Rating { get; private set; }

        public Genres Genres { get; private set; }

        [Required]
        public int MovieLengthMinutes { get; private set; }

        private Movie() { }

        public Movie(MovieModel form)
        {
            if (form == null){throw new ArgumentNullException(nameof(form));}

            if (string.IsNullOrWhiteSpace(form.Name)){throw new ArgumentNullException(nameof(form.Name), 
                "Movie title cannot be null or empty."); }

            if (form.MovieLengthMinutes <= 0){throw new ArgumentOutOfRangeException(nameof(form.MovieLengthMinutes), 
                "Movie duration must be greater than zero minutes.");}

            Name = form.Name;
            Rating = form.Rating;
            Genres = form.Genres;
            MovieLengthMinutes = form.MovieLengthMinutes;
        }

        public void UpdateRating(Ratings newRating)
        {
            Rating = newRating;
        }

        public void UpdateGenre(Genres newGenres)
        {
            Genres = newGenres;
        }

        public class MovieModel
        {
            [Required]
            [StringLength(200)]
            public string Name { get; set; } = null!;

            public Ratings Rating { get; set; } = Ratings.G;

            public Genres Genres { get; set; } = Genres.None;

            [Required]
            [Range(1, 1000)]
            public int MovieLengthMinutes { get; set; }
        }
    }

    public enum Ratings { 
        G, PG, PG13, R
    }

    public enum Genres
    {
        None,
        Aciton, 
        Comedy, 
        Horror, 
        Mystery,
        Romance,
        SciFi,
        Adventure,
        Drama,
        Fantasy,
        Thriller,
        Crime,
        Documentary,
        Animation,
        Family,

    }

}