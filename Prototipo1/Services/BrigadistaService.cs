using Microsoft.EntityFrameworkCore;
using Prototipo1.Context;
using Prototipo1.DTOs.Reportes;
using Prototipo1.DTOs.Ubicaciones;

namespace Prototipo1.Services
{
    public class BrigadistaService
    {
        private readonly AppDbContext _context;

        public BrigadistaService(AppDbContext context)
        {
            _context = context;
        }

        // 1. Listar reportes pendientes (aún no aceptados por ningún brigadista)
        public async Task<List<ReporteResumenDto>> ObtenerReportesPendientes()
        {
            return await _context.Reportes
                .Where(r => r.Estado == "Pendiente")
                .Select(r => new ReporteResumenDto
                {
                    IdReporte = r.IdReporte,
                    Ubicacion = new UbicacionDto
                    {
                        IdUbicacion = r.Ubicacion.IdUbicacion,
                        Nombre = r.Ubicacion.Nombre
                    },
                    Descripcion = r.Descripcion,
                    Estado = r.Estado,
                    HoraCreacion = r.HoraCrecacion,
                    FechaCreacion = r.FechaCreacion
                }).ToListAsync();
        }

        // 2. Aceptar un reporte: lo asigna al brigadista y cambia el estado a "En proceso"
        public async Task<bool> AceptarReporte(int idReporte, int idBrigadista)
        {
            var reporte = await _context.Reportes
                .FirstOrDefaultAsync(r => r.IdReporte == idReporte && r.Estado == "Pendiente");

            if (reporte == null || reporte.IdUsuario == idBrigadista)
                return false;

            
            reporte.IdUsuarioBrigadista = idBrigadista;
            reporte.Estado = "En proceso";
            var now = DateTime.Now;
            reporte.FechaCreacion = DateOnly.FromDateTime(now);
            reporte.HoraCrecacion = TimeOnly.FromDateTime(now);

            await _context.SaveChangesAsync();
            return true;
        }

        // 3. Finalizar un reporte (solo el brigadista asignado puede hacerlo)
        public async Task<bool> FinalizarReporte(int idReporte, int idBrigadista)
        {
            var reporte = await _context.Reportes
                .FirstOrDefaultAsync(r =>
                    r.IdReporte == idReporte &&
                    r.IdUsuarioBrigadista == idBrigadista &&
                    r.Estado == "En proceso");

            if (reporte == null)
                return false;

            var now = DateTime.Now;
            reporte.Estado = "Finalizado";
            reporte.FechaCreacion = DateOnly.FromDateTime(now);
            reporte.HoraCrecacion = TimeOnly.FromDateTime(now);
            await _context.SaveChangesAsync();
            return true;
        }

        // 4. Ver todos los reportes asignados al brigadista
        public async Task<List<ReporteResumenDto>> ObtenerMisReportesAsignados(int idBrigadista)
        {
            return await _context.Reportes
                .Where(r => r.IdUsuarioBrigadista == idBrigadista)
                .Select(r => new ReporteResumenDto
                {
                    IdReporte = r.IdReporte,
                    Ubicacion = new UbicacionDto
                    {
                        IdUbicacion = r.Ubicacion.IdUbicacion,
                        Nombre = r.Ubicacion.Nombre
                    },
                    Descripcion = r.Descripcion,
                    Estado = r.Estado,
                    HoraCreacion = r.HoraCrecacion,
                    FechaCreacion = r.FechaCreacion
                }).ToListAsync();
        }
    }
}
