﻿using Prototipo1.DTOs.Ubicaciones;

namespace Prototipo1.DTOs.Reportes
{
    public class DetalleReporteDto
    {
        public int IdReporte { get; set; }
        public UbicacionDto Ubicacion { get; set; } = new();
        public string Descripcion { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string? BrigadistaCorreo { get; set; }
        public DateOnly FechaCreacion { get; set; }
        public TimeOnly HoraCreacion { get; set; }
    }
}
    