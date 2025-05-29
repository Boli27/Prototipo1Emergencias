namespace Prototipo1.DTOs.Ubicaciones
{
    public class UbicacionCreateDto
    {
        public required string Nombre { get; set; }
        public required string Descripcion { get; set; }
        public required string Sede { get; set; }
        public required string Edificio { get; set; }
        public required string Salon { get; set; }
        public required string InformacionAdicional { get; set; }
    }

}
