namespace PawApi.Controllers.DTOs
{
    public class ReseniumDto
    {
        public int Id { get; set; }
        public string Resenia { get; set; }
        public decimal? Puntuacion { get; set; }
        public int? EstadiaId { get; set; }
    }
}