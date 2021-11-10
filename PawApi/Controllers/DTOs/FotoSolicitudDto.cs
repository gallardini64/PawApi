namespace PawApi.Controllers.DTOs
{
    public class FotoSolicitudDto
    {
        public int Id { get; set; }
        public int? FotoId { get; set; }
        public int? SolicitudId { get; set; }

        public FotoDto Foto { get; set; }
    }
}