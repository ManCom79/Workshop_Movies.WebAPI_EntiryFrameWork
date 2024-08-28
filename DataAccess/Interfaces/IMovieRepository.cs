using DomainModels;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IMovieRepository
    {
        public void Add(Movie movie);
        public List<Movie> GetAll();
        public Movie GetById(int id);
        public void Update(MovieWithIdDto movie);
        public void Delete(Movie movie);
        public void DeleteById (int id);
        
    }
}
