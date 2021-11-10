using System;

namespace PawApi.Controllers.DTOs
{
    public class PagoDto
    {
        public int Id { get; set; }
        public int? EstadiaId { get; set; }
        public DateTime? FechaEmitido { get; set; }
        public DateTime? FechaPagado { get; set; }
        public decimal? Total { get; set; }
        public decimal? Importe { get; set; }
        public decimal? Comision { get; set; }
        public string NumeroRefencia { get; set; }
        public string Estado { get; set; }
        public int? MetodoId { get; set; }
    }
}