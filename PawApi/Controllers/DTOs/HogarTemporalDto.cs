namespace PawApi.Controllers.DTOs
{
    public class HogarTemporalDto
    {
        public int Id { get; set; }
        public string Direccion { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }
        public int? TipoHogarId { get; set; }
    }
}