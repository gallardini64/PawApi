using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PawApi.Controllers.DTOs
{
    public class SolicitudDto
    {
        public int Id { get; set; }
        public int? CuidadorId { get; set; }
        public string Peticion { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public DateTime? FechaVideoLlamada { get; set; }
        public string DniSolicitud { get; set; }
        public string Observaciones { get; set; }
        public string MotivoRechazo { get; set; }
        public virtual IEnumerable<FotoSolicitudDto> FotoSolicituds { get; set; }
    }
}
