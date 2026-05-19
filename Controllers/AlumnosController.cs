using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Alumnos.API.Models;
using Alumnos.API.DTOs;

namespace Alumnos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnosController : ControllerBase
    {
        // Por convención profesional, las variables inyectadas empiezan con un guion bajo(_)
        private readonly GestionAlumnosContext _db;

        // El constructor: ASP.NET le "inyecta" magicamente el contexto cuando crea el controlador
        public AlumnosController(GestionAlumnosContext db)
        {
            _db = db;
        }

        // Metodo GET: Para traer datos (Equivale al "Mostrar Alumnos")
        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var listaAlumnos = _db.Alumnos.ToList();
            return Ok(listaAlumnos); // El "Ok" es el codigo 200 de internet
        }

        // Metodo POST: Para ENVIAR datos (Equivale al "Agregar Alumnos")
        [HttpPost]
        public IActionResult Agregar([FromBody] AlumnoDTO alumnoDto)
        {
            // 1. Recibimos el DTO (Limpio y validado)
            // 2. Armamos la entidad real que va a ir a la base de datos
            Alumno nuevoAlumno = new Alumno(
                alumnoDto.Nombre,
                alumnoDto.Legajo,
                alumnoDto.Promedio,
                alumnoDto.Carrera
            );
            
            _db.Alumnos.Add(nuevoAlumno);
            _db.SaveChanges(); // Guardamos en la base de datos

            return Ok("¡Alumno agregado a la base de datos!");
        }

        // Metodo GET por ID: Para buscar un alumno en particular
        [HttpGet("{legajo}")]
        public IActionResult ObtenerPorLegajo(int legajo)
        {
            // Usamos LINQ para buscar el primero que coincida con el legajo
            var alumno = _db.Alumnos.FirstOrDefault(a => a.Legajo == legajo);

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
            var alumno = _db.Alumnos.FirstOrDefault(a => a.Legajo == legajo);

            if (alumno == null)
            {
                return NotFound("El alumno no existe y no se puede eliminar");
            }

            _db.Alumnos.Remove(alumno); // Lo borramos del borrador
            _db.SaveChanges(); // Confirmamos el borrado en la base de datos

            return Ok("¡Alumno borrado correctamente!"); 
        }
    }
}
