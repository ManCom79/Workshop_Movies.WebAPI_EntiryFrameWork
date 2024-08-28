using DataAccess.Interfaces;
using DomainModels;
using DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implementations
{
    public class MovieRepository : IMovieRepository
    {
        public readonly MoviesDbContext _dbContext;
        public MovieRepository(MoviesDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Movie movie)
        {
            _dbContext.Movies.Add(movie);
            _dbContext.SaveChanges();
        }

        public List<Movie> GetAll()
        {
            var movies = _dbContext.Movies.ToList();
            if (movies != null) 
            {
                return movies;
            } else
            {
                movies = new List<Movie>();
                return movies;
            }
            
        }

        public Movie GetById(int id)
        {
            var movie = _dbContext.Movies.Where(x => x.Id == id).SingleOrDefault();
            return movie;
        }

        public void Update(MovieWithIdDto model)
        {
            var movie = _dbContext.Movies.Where(x => x.Id == model.Id).SingleOrDefault();

            movie.Id = model.Id;
            movie.Title = model.Title;
            movie.Description = model.Description;
            movie.Year = model.Year;
            movie.Genre = movie.Genre;

            _dbContext.Movies.Update(movie);
            _dbContext.SaveChanges();
        }

        public void Delete(Movie movie)
        {
            _dbContext.Movies.Remove(movie);
            _dbContext.SaveChanges();
        }

        public void DeleteById(int id)
        {
            var movieToRemove = _dbContext.Movies.Where(x => x.Id == id).FirstOrDefault();
            Delete(movieToRemove);
            _dbContext.SaveChanges();
        }
    }
}
