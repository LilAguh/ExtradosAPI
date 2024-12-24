
using DataAccess.Models;
using ExtradosApi.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Interfaces;
using DataAccess.Implementations;
using ExtradosApi.Services.Interfaces;

namespace ExtradosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAllUsersRegistered()
        {
            try
            {
                var users = _userService.GetAllUsersRegistered();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("active")]
        public IActionResult GetAllActiveUsers()
        {
            try
            {
                var users = _userService.GetAllActiveUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var user = _userService.GetUser(id);
                if (user == null)
                    return NotFound("User not found.");
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            try
            {
                var newUser = _userService.CreateUser(user.Name, user.Mail, user.Age);
                return CreatedAtAction(nameof(GetUserById), new { id = newUser.ID }, newUser);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            try
            {
                var updatedUser = _userService.UpdateUser(id, user.Name, user.Age, user.Mail);
                return Ok(updatedUser);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeactivateUser(int id)
        {
            try
            {
                var result = _userService.DeactivateUser(id);
                return result ? Ok("User deactivated successfully.") : NotFound("User not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}