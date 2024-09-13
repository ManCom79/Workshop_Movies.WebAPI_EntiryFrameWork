using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Implementations;
using Services.Interfaces;

namespace Workshop_Movies.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("RegisterUser")]
        public IActionResult RegisterUser([FromBody] UserDto userDto)
        {
            try
            {
                var addUserResult = _userService.AddUser(userDto);
                if(addUserResult < 0)
                {
                    return BadRequest("An error occured!");
                }
                return StatusCode(StatusCodes.Status201Created, "User is successfully registered!");
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [AllowAnonymous]
        [HttpPost("UserLogIn")]
        public IActionResult UserLogIn([FromBody] UsernamePassDto userNamePassDto)
        {
            try
            {
                LoggedUserDTO user = _userService.LogIn(userNamePassDto.UserName, userNamePassDto.Password);
                return Ok(user);
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
