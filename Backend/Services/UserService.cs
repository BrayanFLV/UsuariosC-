using Backend.Data;
using Backend.Models;
using Backend.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users.Select(MapToDto).ToList();
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user == null ? null : MapToDto(user);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UpdateUserAsync(int id, User updatedUser)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            user.Nombre = updatedUser.Nombre;
            user.Apellidos = updatedUser.Apellidos;
            user.Cedula = updatedUser.Cedula;
            user.Correo = updatedUser.Correo;
            user.FechaUltimoAcceso = updatedUser.FechaUltimoAcceso;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        // ✅ NUEVO: Actualizar fecha de último acceso por correo
        public async Task<UserDto?> UpdateLastAccessByEmailAsync(string correo)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Correo == correo);
            if (user == null) return null;

            user.FechaUltimoAcceso = DateTime.Now;
            await _context.SaveChangesAsync();

            return MapToDto(user);
        }

        // 🔐 MÉTODO PRIVADO PARA CALCULAR CLASIFICACIÓN Y PUNTAJE
        private UserDto MapToDto(User user)
        {
            var nombreCompleto = (user.Nombre + " " + user.Apellidos).Trim();
            var dominioCorreo = user.Correo.Split('@').LastOrDefault()?.ToLower() ?? "";

            // Clasificación por fecha de último acceso
            var horasDesdeUltimoAcceso = (DateTime.Now - user.FechaUltimoAcceso).TotalHours;
            string clasificacion = horasDesdeUltimoAcceso switch
            {
                < 12 => "Hechicero",
                < 48 => "Luchador",
                <= 168 => "Explorador", // 7 días
                _ => "Olvidado"
            };

            // Puntaje por nombre y dominio de correo
            int puntaje = 0;
            int longitudNombre = nombreCompleto.Length;

            puntaje += longitudNombre > 10 ? 20 :
                       longitudNombre >= 5 ? 10 : 0;

            puntaje += dominioCorreo switch
            {
                "gmail.com" => 40,
                "hotmail.com" => 20,
                _ => 10
            };

            return new UserDto
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Apellidos = user.Apellidos,
                Cedula = user.Cedula,
                Correo = user.Correo,
                FechaUltimoAcceso = user.FechaUltimoAcceso,
                Clasificacion = clasificacion,
                Puntaje = puntaje
            };
        }
    }
}
