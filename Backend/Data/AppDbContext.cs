
using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) //  Constructor que recibe las opciones de configuración
            : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
