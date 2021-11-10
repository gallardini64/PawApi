using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PawApi.Controllers.DTOs
{
    public class ContratoDto
    {
        public int? PersonaId { get; set; }
        public int? CuidadorId { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? CantidadMacotas { get; set; }
        public int? HogarId { get; set; }
        public PagoDto Pago { get; set; }
    }
}
