// Models/User.cs
using System;

namespace Backend.Models // Definición de la clase User que representa un usuario en el sistema
// Se utiliza para mapear la tabla de usuarios en la base de datos
{
    public class User
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public DateTime FechaUltimoAcceso { get; set; }
    }
}
