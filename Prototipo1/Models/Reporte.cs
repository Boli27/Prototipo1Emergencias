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

        // Relaciones
        public Usuario Usuario { get; set; }              // Quien reporta
        public Usuario UsuarioBrigadista { get; set; }    // Quien atiende
        public Ubicacion Ubicacion { get; set; }
    }
}
