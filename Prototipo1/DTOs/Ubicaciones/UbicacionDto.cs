namespace Prototipo1.DTOs.Ubicaciones
{
    public class UbicacionDto
    {
        public int IdUbicacion { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Sede { get; set; } = string.Empty;
        public string Edificio { get; set; } = string.Empty;
        public string Salon { get; set; } = string.Empty;
        public string InformacionAdicional { get; set; } = string.Empty;
        public List <ReporteResumenUbiDto>? Reportes { get; set; }
    }
}
