using DTOs;
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

        [HttpPost("UserLogIn")]
        public IActionResult UserLogIn([FromBody] UserDto userDto)
        {
            try
            {
                LoggedUserDTO user = _userService.LogIn(userDto.UserName, userDto.Password);
                return Ok(user);
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
