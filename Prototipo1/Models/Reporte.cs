using System.ComponentModel.DataAnnotations;

namespace Prototipo1.Models
{
    public class Reporte
    {
        [Key]
        public int IdReporte { get; set; }

        // FKs
        public required int IdUsuario { get; set; }

        public int? IdUsuarioBrigadista { get; set; }  // Puede ser null al momento de la creación

        public required int IdUbicacion { get; set; }

        // Datos del reporte
        public string? Estado { get; set; }
        public required string Descripcion { get; set; }

        // Tiempo de creacion
        [DataType(DataType.Date)]
        public DateOnly FechaCreacion { get; set; }
        [DataType(DataType.Time)]
        public TimeOnly HoraCrecacion { get; set; }

        // Tiempos de cancelacion
        [DataType(DataType.Date)]
        public DateOnly FechaCancelacion { get; set; }
        [DataType(DataType.Time)]
        public TimeOnly HoraCancelacion { get; set; }

        // Tiempos de aceptacion
        [DataType(DataType.Date)]
        public DateOnly FechaAceptado { get; set; }
        [DataType(DataType.Time)]
        public TimeOnly HoraAceptado { get; set; }

        // Tiempos de finalizacion
        [DataType(DataType.Date)]
        public DateOnly FechaFinalizacion { get; set; }
        [DataType(DataType.Time)]
        public TimeOnly HoraFinalizacion { get; set; }


        // Relaciones
        public Usuario Usuario { get; set; }              // Quien reporta
        public Usuario UsuarioBrigadista { get; set; }    // Quien atiende
        public Ubicacion Ubicacion { get; set; }
    }
}
