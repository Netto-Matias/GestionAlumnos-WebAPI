using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Alumnos.API.Models;
using Alumnos.API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Alumnos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // <-- Esto cierra la puerta por completo
    public class AlumnosController : ControllerBase
    {
        // Por convención profesional, las variables inyectadas empiezan con un guion bajo(_)
        private readonly GestionAlumnosContext _db;

        // El constructor: ASP.NET le "inyecta" magicamente el contexto cuando crea el controlador
        public AlumnosController(GestionAlumnosContext db)
        {
            _db = db;
        }

        // Metodo GET
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var listaAlumnos = await _db.Alumnos.ToListAsync();
            return Ok(listaAlumnos); 
        }

        // Metodo POST
        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] AlumnoDTO alumnoDto)
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

            await _db.SaveChangesAsync(); // Guardamos en la base de datos

            return Ok("¡Alumno agregado a la base de datos!");
        }

        // Metodo GET por ID: Para buscar un alumno en particular
        [HttpGet("{legajo}")]
        public async Task<IActionResult> ObtenerPorLegajo(int legajo)
        {
            // LINQ para buscar el primero que coincida con el legajo
            var alumno = await _db.Alumnos.FirstOrDefaultAsync(a => a.Legajo == legajo);

            if (alumno == null)
            {
                return NotFound("No se encontró ningún alumno con ese legajo.");
            }

            return Ok(alumno);
        }

        // Método DELETE: Para dar de baja a un alumno
        [HttpDelete("{legajo}")]
        public async Task<IActionResult> Eliminar(int legajo)
        {
            var alumno = await _db.Alumnos.FirstOrDefaultAsync(a => a.Legajo == legajo);

            if (alumno == null)
            {
                return NotFound("El alumno no existe y no se puede eliminar");
            }

            _db.Alumnos.Remove(alumno); // Lo borramos del borrador
            await _db.SaveChangesAsync(); // Confirmamos el borrado en la base de datos

            return Ok("¡Alumno borrado correctamente!"); 
        }
    }
}
