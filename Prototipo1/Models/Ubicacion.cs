using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prototipo1.Models
{
    public class Ubicacion
    {
        [Key]
        public int IdUbicacion { get; set; }
        public required string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public required string Sede { get; set; }
        public string? Edificio { get; set; }
        public string? Salon { get; set; }
        public string? InformacionAdicional { get; set; }

        // Relación con Reporte
        public ICollection<Reporte> Reportes { get; set; }
    }
}
