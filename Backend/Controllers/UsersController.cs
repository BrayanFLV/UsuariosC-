using Backend.DTOs;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _service;

        public UsersController(UserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _service.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _service.GetUserByIdAsync(id);
            if (user == null) return NotFound("Usuario no encontrado");
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            var created = await _service.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User user)
        {
            var updated = await _service.UpdateUserAsync(id, user);
            if (!updated) return NotFound("Usuario no encontrado");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteUserAsync(id);
            if (!deleted) return NotFound("Usuario no encontrado");
            return NoContent();
        }

        // ✅ Login - actualizar fecha de último acceso por correo
        [HttpPut("login")]
        public async Task<IActionResult> LoginUpdate([FromBody] LoginRequest request)
        {
            var user = await _service.UpdateLastAccessByEmailAsync(request.Correo);
            if (user == null) return NotFound("Usuario no encontrado");
            return Ok(user);
        }
    }
}

