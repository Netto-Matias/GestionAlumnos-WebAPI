using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Alumnos.API.Models;

namespace Alumnos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnosController : ControllerBase
    {
        // Instancio el traductor de base de datos como lo hacia en el modelo viejo 
        GestionAlumnosContext db = new GestionAlumnosContext();

        // Metodo GET: Para traer datos (Equivale al "Mostrar Alumnos")
        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var listaAlumnos = db.Alumnos.ToList();
            return Ok(listaAlumnos); // El "Ok" es el codigo 200 de internet
        }

        // Metodo POST: Para ENVIAR datos (Equivale al "Agregar Alumnos")
        [HttpPost]
        public IActionResult Agregar([FromBody] Alumno nuevoAlumno)
        {
            db.Alumnos.Add(nuevoAlumno);
            db.SaveChanges(); // Guardamos en la base de datos
            return Ok("¡Alumno agregado a la base de datos!");
        }

        // Metodo GET por ID: Para buscar un alumno en particular
        [HttpGet("{legajo}")]
        public IActionResult ObtenerPorLegajo(int legajo)
        {
            // Usamos LINQ para buscar el primero que coincida con el legajo
            var alumno = db.Alumnos.FirstOrDefault(a => a.Legajo == legajo);

            if (alumno == null)
            {
                return NotFound("No se encontró ningún alumno con ese legajo.");
            }

            return Ok(alumno);
        }

        // Método DELETE: Para dar de baja a un alumno
        [HttpDelete("{legajo}")]
        public IActionResult Eliminar(int legajo)
        {
            var alumno = db.Alumnos.FirstOrDefault(a => a.Legajo == legajo);

            if (alumno == null)
            {
                return NotFound("El alumno no existe y no se puede eliminar");
            }

            db.Alumnos.Remove(alumno); // Lo borramos del borrador
            db.SaveChanges(); // Confirmamos el borrado en la base de datos

            return Ok("¡Alumno borrado correctamente!"); 
        }
    }
}
