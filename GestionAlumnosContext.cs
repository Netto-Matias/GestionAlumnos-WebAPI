using Alumnos.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Alumnos.API
{
    public class GestionAlumnosContext : DbContext
    {
        // Creo el constructor nuevo. Recibe las opciones y se las pasa a la clase base (DbContext)
        public GestionAlumnosContext(DbContextOptions<GestionAlumnosContext> options) : base(options)
        {

        }
        public DbSet<Alumno> Alumnos { get; set; }
        
    }
}
