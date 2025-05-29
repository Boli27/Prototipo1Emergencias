using System.ComponentModel.DataAnnotations;

namespace Prototipo1.Models
{
    public class Administrador
    {
        [Key]
        public int IdAdministrador { get; set; }

        public required string Correo { get; set; }

        public required string Contraseña { get; set; }
    }
}
