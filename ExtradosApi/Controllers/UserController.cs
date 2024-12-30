
using DataAccess.Models;
using ExtradosApi.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using DataAccess.Interfaces;
using DataAccess.Implementations;
using ExtradosApi.Services.Interfaces;
using BCrypt.Net;

namespace ExtradosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public UserController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpGet]
        [Authorize]
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
        [Authorize]
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

        [HttpPost("login")]
        public IActionResult GetUser([FromBody] Login login)
        {
            try
            {
                var user = _userService.GetUser(login.Mail, login.Password);
                var token = _jwtService.GenerateToken(user.ID.ToString(), user.Mail);
                return Ok(new{ user, token });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var user = _userService.GetUserById(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            try
            {
                // Validar que Age no sea null
                if (!user.Age.HasValue)
                    throw new ArgumentException("Age is required.");

                var newUser = _userService.CreateUser(user.Name, user.Password, user.Mail, user.Age.Value);
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
                var updatedUser = _userService.UpdateUser(id, user.Name, user.Age, user.Mail, user.Password);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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