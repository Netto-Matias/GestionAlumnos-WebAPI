using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alumnos.API.DTOs
{
    public class AlumnoDTO
    {
        [Required(ErrorMessage = "¡El nombre no puede estar vacío!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres.")]
        public string Nombre { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Range(1000, 99999, ErrorMessage = "El legajo debe ser un numero válido entre 1000 y 99999")]
        public int Legajo { get; set; }

        [Range(1, 10, ErrorMessage = "El promedio debe ser una nota válda entre 1 y 10.")]
        public double Promedio { get; set; }

        [Required(ErrorMessage = "Debes especificar la carrera del alumno.")]
        public string Carrera { get; set; }
    }
}
