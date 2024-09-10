using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUserService
    {
        public int AddUser(UserDto userDto);
        public LoggedUserDTO LogIn(string username, string password);
    }
}
