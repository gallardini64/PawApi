namespace PawApi.Controllers.DTOs
{
    public class CuidadorMascotaDto
    {
        public int Id { get; set; }
        public int? EspecieId { get; set; }
        public int? Cantidad { get; set; }
        public decimal? PrecioUnitarioDiario { get; set; }
    }
}