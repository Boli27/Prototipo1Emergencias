using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Prototipo1.Models;

namespace Prototipo1.Helpers
{
    public static class JwtHelper
    {
        public static string GenerarToken(Usuario usuario, string secretKey)
        {
            var claims = new[]
            {
                new Claim("id", usuario.IdUsuario.ToString()),
                new Claim("correo", usuario.CorreoInstitucional),
                new Claim("EsBrigadista", usuario.EsBrigadista.ToString().ToLower())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "Prototipo1",
                audience: "Prototipo1",
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
