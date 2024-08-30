using DataAccess.Interfaces;
using DomainModels;
using DomainModels.Enums;
using DTOs;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class MovieService : IMovieService
    {
        public readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public string CreateMovie(MovieDto movie)
        {
            var movieExist = _movieRepository.GetAll().Where(x => x.Title == movie.Title).FirstOrDefault();

            Genres genre;

            if (movieExist == null)
            {
                if (Enum.TryParse(movie.Genre, true, out genre))
                {
                    var newMovie = new Movie()
                    {
                        //Id = nextId,
                        Title = movie.Title,
                        Year = movie.Year,
                        Description = movie.Description,
                        Genre = genre
                    };

                    _movieRepository.Add(newMovie);
                }
                return $"Movie {movie.Title} is added to the list.";
            }

            return $"Movie {movie.Title} already exist.";
        }

        public List<MovieDto> GetAllMovies()
        {
            var movies = _movieRepository.GetAll();
            var moviesDtos = movies.Select(x => new MovieDto
            {
                Title = x.Title,
                Year = x.Year,
                Description = x.Description,
                Genre = x.Genre.ToString(),
            }).ToList();
            return moviesDtos;
        }

        public MovieWithIdDto GetMovieById(int id)
        {
            var movie = _movieRepository.GetById(id);

            if (movie != null)
            {
                var movieDto = new MovieWithIdDto
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Year = movie.Year,
                    Description = movie.Description,
                    Genre = movie.Genre.ToString()
                };
                return movieDto;
            } else
            {
                return new MovieWithIdDto();
            }
        }

        public List<MovieDto> GetMovieByGenreOrYear(string? genre, int? year)
        {
            if (genre != null && year != null)
            {
                var movies = GetAllMovies().Where(x => x.Genre.ToString().ToLower().Equals(genre.ToLower()) && x.Year.Year == year);
                var movieDtos = movies.Select(x => new MovieDto
                {
                    Title = x.Title,
                    Year = x.Year,
                    Description = x.Description,
                    Genre = x.Genre.ToString()
                }).ToList();
                return movieDtos;
            } else if (genre != null && year == null)
            {
                var movies = GetAllMovies().Where(x => x.Genre.ToString().ToLower().Equals(genre.ToLower()));
                var movieDtos = movies.Select(x => new MovieDto
                {
                    Title = x.Title,
                    Year = x.Year,
                    Description = x.Description,
                    Genre = x.Genre.ToString()
                }).ToList();
                return movieDtos;
            }
            else if (genre == null && year != null)
            {
                var movies = GetAllMovies().Where(x => x.Year.Year == year);
                var movieDtos = movies.Select(x => new MovieDto
                {
                    Title = x.Title,
                    Year = x.Year,
                    Description = x.Description,
                    Genre = x.Genre.ToString()
                }).ToList();
                return movieDtos;
            } else
            {
                return new List<MovieDto>();
            }
        }

        public string UpdateMovie(MovieWithIdDto movie)
        {
            Genres genre;

            var idToCheckIfMovieExist = movie.Id;
            var movieExist = _movieRepository.GetById(idToCheckIfMovieExist);

            if (movieExist != null)
            {
                if(Enum.TryParse(movie.Genre, true, out genre))
                {
                    var updatedMovie = new Movie()
                    {
                        Id = movieExist.Id,
                        Title = movie.Title,
                        Year = movieExist.Year,
                        Description = movieExist.Description,
                        Genre = genre
                    };

                    _movieRepository.Update(movie);

                    return $"Movie {movie.Title} is updated";
                } else
                {
                    return $"Please use allowed values for genre.";
                }
            } else
            {
                return $"Movie with Id: {movie.Id} does not exist.";
            }
        }

        public string DeleteMovie(int id)
        {
            var movieExist = _movieRepository.GetById(id);

            if (movieExist != null)
            {
                _movieRepository.Delete(movieExist);
                return $"Movie {movieExist.Title} with Id: {id} was succesfully deleted.";
            } else
            {
                return $"Movie with Id: {id} does not exist";
            }
        }

    }
}
