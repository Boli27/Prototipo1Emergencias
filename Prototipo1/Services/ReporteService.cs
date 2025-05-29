using Prototipo1.Context;
using Prototipo1.DTOs.Reportes;
using Prototipo1.DTOs.Ubicaciones;
using Prototipo1.Models;
using Microsoft.EntityFrameworkCore;

namespace Prototipo1.Services
{
    public class ReporteService
    {
        private readonly AppDbContext _context;

        public ReporteService(AppDbContext context)
        {
            _context = context;
        }

        // Método para crear un nuevo reporte
        public async Task<Reporte?> CrearReporte(int idUsuario, CrearReporteDto dto)
        {
            // Cuenta cuántos reportes activos (no finalizados ni cancelados) tiene el usuario
            var cantidadPendientes = await _context.Reportes
                .CountAsync(r => r.IdUsuario == idUsuario && r.Estado != "Finalizado" && r.Estado != "Cancelado");

            // Si ya tiene un reporte activo, no se permite crear otro
            if (cantidadPendientes >= 1)
                return null;

            // Crea el objeto Reporte
            var reporte = new Reporte
            {
                IdUsuario = idUsuario,
                IdUbicacion = dto.IdUbicacion,
                Descripcion = dto.Descripcion,
                Estado = "Pendiente" // Estado inicial
            };

            // Agrega el nuevo reporte al contexto y guarda en la base de datos
            _context.Reportes.Add(reporte);
            await _context.SaveChangesAsync();

            return reporte;
        }

        // Método para obtener la lista de reportes del usuario
        public async Task<List<ReporteResumenDto>> ObtenerMisReportes(int idUsuario)
        {
            return await _context.Reportes
                .Where(r => r.IdUsuario == idUsuario)
                .Select(r => new ReporteResumenDto
                {
                    IdReporte = r.IdReporte,
                    Ubicacion = new UbicacionDto
                    {
                        IdUbicacion = r.Ubicacion.IdUbicacion,
                        Nombre = r.Ubicacion.Nombre,
                        Sede = r.Ubicacion.Sede,
                        Edificio = r.Ubicacion.Edificio,
                        Salon = r.Ubicacion.Salon
                    },
                    Descripcion = r.Descripcion,
                    Estado = r.Estado
                }).ToListAsync();
        }

        // Método para obtener los detalles de un reporte específico
        public async Task<DetalleReporteDto?> ObtenerReporte(int idReporte, int idUsuario)
        {
            var reporte = await _context.Reportes
                .Include(r => r.UsuarioBrigadista) // Incluye los datos del brigadista (si hay uno asignado)
                .Include(r => r.Ubicacion)         // Incluye los datos de la ubicación
                .FirstOrDefaultAsync(r => r.IdReporte == idReporte && r.IdUsuario == idUsuario);

            if (reporte == null)
                return null;

            // Retorna los datos detallados del reporte
            return new DetalleReporteDto
            {
                IdReporte = reporte.IdReporte,
                Ubicacion = new UbicacionDto
                {
                    IdUbicacion = reporte.Ubicacion.IdUbicacion,
                    Nombre = reporte.Ubicacion.Nombre,
                    Sede = reporte.Ubicacion.Sede,
                    Edificio = reporte.Ubicacion.Edificio,
                    Salon = reporte.Ubicacion.Salon
                },
                Descripcion = reporte.Descripcion,
                Estado = reporte.Estado,
                BrigadistaCorreo = reporte.UsuarioBrigadista?.CorreoInstitucional
            };
        }

        // Método para cancelar un reporte en estado "Pendiente"
        public async Task<bool> CancelarReporte(int idReporte, int idUsuario)
        {
            // Busca el reporte que pertenece al usuario y que esté en estado "Pendiente"
            var reporte = await _context.Reportes
                .FirstOrDefaultAsync(r => r.IdReporte == idReporte && r.IdUsuario == idUsuario && r.Estado == "Pendiente");

            // Si no se encuentra, no se puede cancelar
            if (reporte == null)
                return false;

            // Cambia el estado a "Cancelado" y guarda cambios
            reporte.Estado = "Cancelado";
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
