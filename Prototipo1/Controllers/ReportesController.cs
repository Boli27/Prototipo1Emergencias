using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prototipo1.DTOs.Reportes;
using Prototipo1.Services;
using System.Security.Claims;

namespace Prototipo1.Controllers
{
    [Authorize] // Requiere autenticación
    [ApiController]
    [Route("api/reportes")]
    public class ReportesController : ControllerBase
    {
        private readonly ReporteService _reporteService;

        public ReportesController(ReporteService reporteService)
        {
            _reporteService = reporteService;
        }

        // Método privado para obtener el ID del usuario desde el token JWT
        private int ObtenerIdUsuario()
        {
            var idClaim = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(idClaim))
                throw new UnauthorizedAccessException("No se encontró el ID del usuario en el token.");

            return int.Parse(idClaim);
        }

        // Endpoint POST para crear un nuevo reporte
        [HttpPost]
        public async Task<IActionResult> CrearReporte([FromBody] CrearReporteDto dto)
        {
            var idUsuario = ObtenerIdUsuario();
            var reporte = await _reporteService.CrearReporte(idUsuario, dto);

            // Si no se puede crear, retorna 400
            if (reporte == null)
                return BadRequest("No puedes crear más reportes en este momento.");

            return Ok("Reporte creado exitosamente");
        }

        // Endpoint GET para obtener todos los reportes del usuario
        [HttpGet]
        public async Task<IActionResult> MisReportes()
        {
            var idUsuario = ObtenerIdUsuario();
            var reportes = await _reporteService.ObtenerMisReportes(idUsuario);
            return Ok(reportes);
        }

        // Endpoint GET para obtener el detalle de un reporte
        [HttpGet("{id}")]
        public async Task<IActionResult> Detalle(int id)
        {
            var idUsuario = ObtenerIdUsuario();
            var reporte = await _reporteService.ObtenerReporte(id, idUsuario);

            // Si el reporte no existe o no pertenece al usuario, retorna 404
            if (reporte == null)
                return NotFound("Reporte no encontrado");

            return Ok(reporte);
        }

        // Endpoint PUT para cancelar un reporte
        [HttpPut("cancelar/{id}")]
        public async Task<IActionResult> Cancelar(int id)
        {
            var idUsuario = ObtenerIdUsuario();
            var cancelado = await _reporteService.CancelarReporte(id, idUsuario);

            // Si no se puede cancelar, retorna 400
            if (!cancelado)
                return BadRequest("No se puede cancelar el reporte");

            return Ok("Reporte cancelado");
        }
    }
}
