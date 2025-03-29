// DTOs/UserDto.cs
namespace Backend.DTOs
{
    public class UserDto // DTO para representar un usuario
    // Se utiliza para transferir datos entre la aplicación y el cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public DateTime FechaUltimoAcceso { get; set; }

        public string Clasificacion { get; set; } = string.Empty;
        public int Puntaje { get; set; }
    }
}
