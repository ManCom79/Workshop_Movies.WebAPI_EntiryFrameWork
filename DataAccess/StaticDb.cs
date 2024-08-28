using DomainModels;
using DomainModels.Enums;

namespace DataAccess
{
    public static class StaticDb
    {

        public static List<Movie> Movies = new List<Movie>()
        {
            new Movie()
            {
                Id = 1,
                Title = "Fight Club",
                Genre = Genres.Action,
                Year = new DateTime(1999, 10, 15)
            },
            new Movie()
            {
                Id = 2,
                Title = "2001: A Space Odyssey",
                Genre = Genres.SciFi,
                Year = new DateTime(1973, 11, 19)
            },
            new Movie()
            {
                Id = 3,
                Title = "The Hangover",
                Genre = Genres.Comedy,
                Year = new DateTime(2009, 07, 10)
            },
            new Movie()
            {
                Id = 4,
                Title = "Office Space",
                Genre = Genres.Comedy,
                Year = new DateTime(1999, 07, 23)
            },
            new Movie()
            {
                Id = 5,
                Title = "Enter the Dragon",
                Genre = Genres.Action,
                Year = new DateTime(1973, 07, 26)
            },
            new Movie()
            {
                Id = 6,
                Title = "Avatar",
                Genre = Genres.SciFi,
                Year = new DateTime(2009, 12, 10)
            }
        };


}
}
