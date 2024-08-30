using DomainModels.Enums;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Diagnostics.Eventing.Reader;

namespace Workshop_Movies.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        public readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpPost("addMovie")]
        public ActionResult<MovieDto> CreateMovie(MovieDto movie)
        {
            Genres genre;

            if (movie.Year > DateTime.Now)
            {
                return BadRequest("Date of movie release cannot be in future.");
            }

            if (!Enum.TryParse(movie.Genre, true, out genre))
            {
                return BadRequest("Select one of the available values for genre.");
            }

            return Ok(_movieService.CreateMovie(movie));
        }
        [HttpGet]
        public ActionResult<List<MovieDto>> GetAllMovies()
        {
            var movies = _movieService.GetAllMovies();
            if (movies.Count > 0)
            {
                return Ok(movies);
            }
            else
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<MovieDto> GetMovieById(int id)
        {
            var movie = _movieService.GetMovieById(id);
            if (movie.Title != null)
            {
                return Ok(movie);
            }
            else
            {
                return BadRequest($"Movie with Id {id} does not exist or Id was not of type int.");
            }
        }

        [HttpGet("queryById")]
        public ActionResult<MovieDto> GetMovieByIdFromQuery([FromQuery] int id)
        {
            var movie = _movieService.GetMovieById(id);
            if (movie.Title != null)
            {
                return Ok(movie);
            }
            else
            {
                return BadRequest($"Movie with id {id} does not exist.");
            }
        }

        [HttpGet("byGenreOrYear")]
        public ActionResult<List<MovieDto>> getByGenre(string? genre, int? year)
        {
            var movies = _movieService.GetMovieByGenreOrYear(genre, year);

            if (movies.Count != 0)
            {
                return Ok(movies);
            }
            else
            {
                return NotFound("Not found.");
            }
        }

        [HttpPut("UpdateMovieWithId")]
        public ActionResult<MovieDto> UpdateMovie([FromBody] MovieWithIdDto movie)
        {
            try
            {
                return Ok(_movieService.UpdateMovie(movie));

            } catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("DeleteFromBody")]
        public IActionResult DeleteFromBody([FromBody] MovieWithIdDto movie)
        {
            var idOfMovieToDelete = movie.Id;
            return Ok(_movieService.DeleteMovie(idOfMovieToDelete));
        }

        [HttpDelete("deleteById")]
        public ActionResult DeleteMovieById([FromQuery] int id)
        {
            return Ok(_movieService.DeleteMovie(id));
        }

    }
}
