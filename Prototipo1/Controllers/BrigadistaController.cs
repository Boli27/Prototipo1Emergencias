using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prototipo1.Services;
using System.Security.Claims;

namespace Prototipo1.Controllers
{
    [Authorize(Policy = "EsBrigadista")]
    [ApiController]
    [Route("api/brigadista/reportes")]
    public class BrigadistaController : ControllerBase
    {
        private readonly BrigadistaService _brigadistaService;

        public BrigadistaController(BrigadistaService brigadistaService)
        {
            _brigadistaService = brigadistaService;
        }

        private int ObtenerIdBrigadista()
        {
            var idClaim = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(idClaim))
                throw new UnauthorizedAccessException("No se encontró el ID del brigadista.");

            return int.Parse(idClaim);
        }

        // GET: api/brigadista/reportes/pendientes
        [HttpGet("pendientes")]
        public async Task<IActionResult> ReportesPendientes()
        {
            var reportes = await _brigadistaService.ObtenerReportesPendientes();
            return Ok(reportes);
        }

        // PUT: api/brigadista/reportes/aceptar/5
        [HttpPut("aceptar/{id}")]
        public async Task<IActionResult> Aceptar(int id)
        {
            var idBrigadista = ObtenerIdBrigadista();
            var aceptado = await _brigadistaService.AceptarReporte(id, idBrigadista);

            if (!aceptado)
                return BadRequest("No se puede aceptar el reporte.");

            return Ok("Reporte aceptado");
        }

        // PUT: api/brigadista/reportes/finalizar/5
        [HttpPut("finalizar/{id}")]
        public async Task<IActionResult> Finalizar(int id)
        {
            var idBrigadista = ObtenerIdBrigadista();
            var finalizado = await _brigadistaService.FinalizarReporte(id, idBrigadista);

            if (!finalizado)
                return BadRequest("No se puede finalizar el reporte.");

            return Ok("Reporte finalizado");
        }

        // GET: api/brigadista/reportes/asignados
        [HttpGet("asignados")]
        public async Task<IActionResult> MisReportes()
        {
            var idBrigadista = ObtenerIdBrigadista();
            var reportes = await _brigadistaService.ObtenerMisReportesAsignados(idBrigadista);
            return Ok(reportes);
        }
    }
}
