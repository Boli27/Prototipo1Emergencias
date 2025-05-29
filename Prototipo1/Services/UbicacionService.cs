using Microsoft.EntityFrameworkCore;
using Prototipo1.Context;
using Prototipo1.DTOs.Ubicaciones;
using Prototipo1.Models;

namespace Prototipo1.Services
{
    public class UbicacionService
    {
        private readonly AppDbContext _context;

        public UbicacionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UbicacionDto>> ObtenerTodas()
        {
            return await _context.Ubicaciones
                .Select(u => new UbicacionDto
                {
                    IdUbicacion = u.IdUbicacion,
                    Nombre = u.Nombre,
                    Descripcion = u.Descripcion,
                    Sede = u.Sede,
                    Edificio = u.Edificio,
                    Salon = u.Salon,
                    InformacionAdicional = u.InformacionAdicional
                }).ToListAsync();
        }

        public async Task<UbicacionDto?> ObtenerPorId(int id)
        {
            var u = await _context.Ubicaciones
                .Include(u => u.Reportes)
                .FirstOrDefaultAsync(u => u.IdUbicacion == id);

            if (u == null) return null;

            return new UbicacionDto
            {
                IdUbicacion = u.IdUbicacion,
                Nombre = u.Nombre,
                Descripcion = u.Descripcion,
                Sede = u.Sede,
                Edificio = u.Edificio,
                Salon = u.Salon,
                InformacionAdicional = u.InformacionAdicional,
                Reportes = u.Reportes?.Select(r => new ReporteResumenUbiDto
                {
                    IdReporte = r.IdReporte,
                    IdUbicacion = r.IdUbicacion,
                    IdUsuario = r.IdUsuario,
                    IdBrigadista = r.IdUsuarioBrigadista,
                    Estado = r.Estado
                }).ToList()
            };
        }



        public async Task<Ubicacion> Crear(UbicacionCreateDto dto)
        {
            var ubicacion = new Ubicacion
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Sede = dto.Sede,
                Edificio = dto.Edificio,
                Salon = dto.Salon,
                InformacionAdicional = dto.InformacionAdicional
            };

            _context.Ubicaciones.Add(ubicacion);
            await _context.SaveChangesAsync();
            return ubicacion;
        }

        public async Task<bool> Actualizar(int id, UbicacionDto dto)
        {
            var ubicacion = await _context.Ubicaciones.FindAsync(id);
            if (ubicacion == null) return false;

            ubicacion.Nombre = dto.Nombre;
            ubicacion.Descripcion = dto.Descripcion;
            ubicacion.Sede = dto.Sede;
            ubicacion.Edificio = dto.Edificio;
            ubicacion.Salon = dto.Salon;
            ubicacion.InformacionAdicional = dto.InformacionAdicional;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(int id)
        {
            var ubicacion = await _context.Ubicaciones.FindAsync(id);
            if (ubicacion == null) return false;

            _context.Ubicaciones.Remove(ubicacion);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
