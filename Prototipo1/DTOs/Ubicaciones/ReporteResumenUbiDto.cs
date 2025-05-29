namespace Prototipo1.DTOs.Ubicaciones
{
    public class ReporteResumenUbiDto
    {
        public int IdReporte { get; set; }
        public int IdUbicacion { get; set; }
        public int IdUsuario { get; set; }
        public int? IdBrigadista { get; set; } // Puede ser null si aún no se asigna
        public string Estado { get; set; }
    }

}
