using ExtradosApi.DAOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ExtradosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DAOUsers _daousers;
        public UserController()
        {
            _daousers = new DAOUsers();
        }
        [HttpGet]
        public IActionResult GetAll() 
        {
            var users = _daousers.GetAllUsersRegistered();
            return Ok(users);
        }
    }
}
