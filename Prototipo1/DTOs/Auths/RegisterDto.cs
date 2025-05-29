using System.ComponentModel;

namespace Prototipo1.DTOs.Auths
{
    public class RegisterDto
    {
        public required string CorreoInstitucional { get; set; }
        public required string Contraseña { get; set; }
        public required bool EsBrigadista { get; set; }  
    }
}