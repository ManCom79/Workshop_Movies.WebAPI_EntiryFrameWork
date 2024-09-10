using DataAccess.Implementations;
using DataAccess.Interfaces;
using DomainModels;
using DTOs;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class UserService : IUserService
    {
        public readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public int AddUser(UserDto userDto)
        {
            if (string.IsNullOrEmpty(userDto.UserName) ||
                string.IsNullOrEmpty(userDto.Password))
            {
                throw new Exception("Username and password must be provided!");
            }

            if(_userRepository.GetUserByUserName(userDto.UserName) != null)
            {
                throw new Exception($"User with username {userDto.UserName} already exist.");
            }

            MD5 md5CryptoServiceProvider = MD5.Create();

            byte[] dTOpassword = Encoding.ASCII.GetBytes(userDto.Password);
            byte[] hashBytes = md5CryptoServiceProvider.ComputeHash(dTOpassword);

            string hashPassword = Encoding.ASCII.GetString(hashBytes);

            User user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                UserName = userDto.UserName,
                Password = hashPassword
            };

            return _userRepository.Add(user);
        }

        public LoggedUserDTO LogIn(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new Exception("Username and Password must be provided");
            }

            MD5 md5CryptoServiceProvider = MD5.Create();

            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);
            byte[] hashBytes = md5CryptoServiceProvider.ComputeHash(passwordBytes);
            string hashPassword = Encoding.ASCII.GetString(hashBytes);

            var userToLogIn = _userRepository.GetUserByUserName(username);

            if (userToLogIn == null) 
            {
                throw new Exception("User is not found.");
            }

            JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();

            byte[] secretKey = Encoding.ASCII.GetBytes("This is the value of my custom secret");

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(119),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name, username),
                        new Claim("UserFullName", $"{userToLogIn.FirstName}{userToLogIn.LastName}"),
                    })
            };

            SecurityToken token = securityTokenHandler.CreateToken(tokenDescriptor);

            LoggedUserDTO loggedUserDtoModel = new LoggedUserDTO
            {
                FirstName = userToLogIn.FirstName,
                LastName = userToLogIn.LastName,
                UserName = userToLogIn.UserName,
                Token = securityTokenHandler.WriteToken(token)
            };

            return loggedUserDtoModel;
        }
    }
}
