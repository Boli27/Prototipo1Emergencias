using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prototipo1.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        public required string CorreoInstitucional { get; set; }

        public required string Contraseña { get; set; }

        public required bool EsBrigadista { get; set; } = false;

        //Atributos para RefreshToken y mantener inicio de sesiones prolongadas y seguras
        //public string? RefreshToken { get; set; }
        //public DateTime? RefreshTokenExpiryTime { get; set; }


        // Relaciones
        public ICollection<Reporte> ReportesRealizados { get; set; }
        public ICollection<Reporte> ReportesAsignados { get; set; }
    }
}
