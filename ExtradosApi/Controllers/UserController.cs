using ExtradosApi.DAOs;
using ExtradosApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ExtradosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
       
        
        [HttpGet]
        public IActionResult GetAll() 
        {
            var users = DAOUsers.Instance.GetAllUsersActive();
            return Ok(users);
        }

        // Obtener todos los usuarios activos
        [HttpGet("active")]
        public IActionResult GetAllUsersActive()
        {
            var users = DAOUsers.Instance.GetAllUsersActive();
            return Ok(users);
        }

        // Obtener un usuario por ID
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = DAOUsers.Instance.GetUser(id);
            if (user == null) return NotFound($"No se encontró el usuario con ID {id}.");
            return Ok(user);
        }

        // Crear un usuario
        [HttpPost]
        public IActionResult CreateUser([FromBody] Users user)
        {
            if (user.Age <= 14)
                return BadRequest("La edad debe ser mayor a 14 años.");

            if (!user.Mail.Contains("@gmail.com"))
                return BadRequest("El email debe ser un correo de Gmail.");

            var nuevoUsuario = DAOUsers.Instance.CreateUser(user.Name,user.Mail, user.Age);
            return Ok(nuevoUsuario);
        }

        // Actualizar un usuario
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] Users user)
        {
            if (user.Age <= 14)
                return BadRequest("La edad debe ser mayor a 14 años.");

            if (!string.IsNullOrEmpty(user.Mail) && !user.Mail.Contains("@gmail.com"))
                return BadRequest("El email debe ser un correo de Gmail.");

            var usuarioActualizado = DAOUsers.Instance.UpdateUser(id, user.Name, user.Age, user.Mail);
            if (usuarioActualizado == null)
                return NotFound($"No se encontró el usuario con ID {id} para actualizar.");
            return Ok(usuarioActualizado);
        }

        // Desactivar un usuario (borrado lógico)
        [HttpPut("desactivate/{id}")]
        public IActionResult DesactivateUser(int id)
        {
            var success = DAOUsers.Instance.DesactivateUser(id);
            if (!success)
                return NotFound($"No se encontró el usuario con ID {id} para desactivar.");
            return Ok($"Usuario con ID {id} desactivado con éxito.");
        }
    }
}
