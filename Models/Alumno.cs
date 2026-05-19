using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Timers;

namespace Alumnos.API.Models
{
    public class Alumno
    {
        public string Nombre { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Legajo { get; set; }
        public double Promedio { get; set; }
        public string Carrera { get; set; }

        public Alumno(string nombre, int legajo, double promedio, string carrera)
        {
            Nombre = nombre;
            Legajo = legajo;
            Promedio = promedio;
            Carrera = carrera;
        }
    }
}
