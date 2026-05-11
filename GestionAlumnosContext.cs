using Alumnos.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Alumnos.API
{
    public class GestionAlumnosContext : DbContext
    {
        public DbSet<Alumno> Alumnos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=GestionAlumnosDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
