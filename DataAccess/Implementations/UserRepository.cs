using DataAccess.Interfaces;
using DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implementations
{
    public class UserRepository : IUserRepository
    {
        public readonly MoviesDbContext _dbContext;

        public UserRepository(MoviesDbContext moviesDbContext)
        {
            _dbContext = moviesDbContext;
        }
        public int Add(User user)
        {
            _dbContext.Users.Add(user);
            return _dbContext.SaveChanges();
        }

        public User GetUserByUserName(string username)
        {
            return _dbContext.Users.SingleOrDefault(x => x.UserName == username);
        }
    }
}
