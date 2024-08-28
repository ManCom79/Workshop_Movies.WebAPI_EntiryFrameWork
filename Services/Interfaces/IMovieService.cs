using DomainModels;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IMovieService
    {
        public string CreateMovie(MovieDto movie);
        public List<MovieDto> GetAllMovies();
        public MovieWithIdDto GetMovieById(int id);
        public List<MovieDto> GetMovieByGenreOrYear(string? genre, int? year);
        public string UpdateMovie(int id, MovieWithIdDto movie);
        public string DeleteMovie(int id);
    }
}
