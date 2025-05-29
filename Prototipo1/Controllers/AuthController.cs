// Estos "using" importan los espacios de nombres necesarios para usar clases como ControllerBase, IActionResult, etc.
using Microsoft.AspNetCore.Mvc; // Funciones del framework ASP.NET Core para construir APIs (como ControllerBase).
using Prototipo1.Services; // Accede a la lógica de negocio que pusiste en AuthService.
using Prototipo1.Helpers; // Donde está el helper para generar el JWT.
using Microsoft.Extensions.Configuration;
using Prototipo1.DTOs.Auths; // Permite acceder a la configuración (como tu clave secreta de JWT).

namespace Prototipo1.Controllers
{
    // Indica que esta clase es un controlador de API.
    [ApiController]

    // Define la ruta base para todos los endpoints del controlador. Ejemplo: api/auth/login
    [Route("api/auth")]

    public class AuthController : ControllerBase // Hereda de ControllerBase, que es la clase base mínima para APIs REST (sin MVC).
    {
        private readonly AuthService _authService;
        private readonly IConfiguration _configuration;

        // Constructor del controlador, recibe las dependencias que necesita: el servicio de autenticación y la configuración.
        public AuthController(AuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        // Endpoint POST en: api/auth/registro
        [HttpPost("registro")]
        public async Task<IActionResult> Registrar([FromBody] RegisterDto dto)
        {
            // Llama al servicio para registrar al usuario.
            var usuario = await _authService.RegistrarUsuario(dto);

            // Si no se pudo crear (usuario ya existe), devuelve error 400.
            if (usuario == null)
                return BadRequest("El usuario ya existe");

            // Si se crea correctamente, responde con OK (200).
            return Ok("Usuario registrado correctamente");
        }

        // Endpoint POST en: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            // Llama al servicio para validar las credenciales.
            var usuario = await _authService.ValidarUsuario(dto);

            // Si no existe o es inválido, responde con error 401 (Unauthorized).
            if (usuario == null)
                return Unauthorized("Credenciales incorrectas");

            // Si es válido, genera un token JWT y lo devuelve.
            var token = JwtHelper.GenerarToken(usuario, _configuration["Jwt:Key"]!);
            return Ok(new { token }); // Devuelve el token como JSON: { token: "eyJ..." }
        }
    }
}
