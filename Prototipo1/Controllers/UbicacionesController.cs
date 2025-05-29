using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prototipo1.DTOs.Reportes;
using Prototipo1.DTOs.Ubicaciones;
using Prototipo1.Services;

namespace Prototipo1.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/ubicaciones")]
    public class UbicacionesController : ControllerBase
    {
        private readonly UbicacionService _ubicacionService;

        public UbicacionesController(UbicacionService ubicacionService)
        {
            _ubicacionService = ubicacionService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodas()
        {
            var ubicaciones = await _ubicacionService.ObtenerTodas();
            return Ok(ubicaciones);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var ubicacion = await _ubicacionService.ObtenerPorId(id);
            if (ubicacion == null) return NotFound("Ubicación no encontrada");

            return Ok(ubicacion);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] UbicacionCreateDto dto)
        {
            var nuevaUbicacion = await _ubicacionService.Crear(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevaUbicacion.IdUbicacion }, nuevaUbicacion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] UbicacionDto dto)
        {
            var actualizado = await _ubicacionService.Actualizar(id, dto);
            if (!actualizado) return NotFound("Ubicación no encontrada");

            return Ok("Ubicación actualizada");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var eliminado = await _ubicacionService.Eliminar(id);
            if (!eliminado) return NotFound("Ubicación no encontrada");

            return Ok("Ubicación eliminada");
        }
    }
}
