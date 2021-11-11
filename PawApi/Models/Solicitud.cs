using System;
using System.Collections.Generic;

#nullable disable

namespace PawApi.Models
{
    public partial class Solicitud : IEntityBase
    {
        public Solicitud()
        {
            FotoSolicituds = new HashSet<FotoSolicitud>();
        }

        public int Id { get; set; }
        public int? CuidadorId { get; set; }
        public int? UsuarioId { get; set; }
        public string Peticion { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public DateTime? FechaVideoLlamada { get; set; }
        public string DniSolicitud { get; set; }
        public string Observaciones { get; set; }
        public string MotivoRechazo { get; set; }
        public virtual Cuidador Cuidador { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<FotoSolicitud> FotoSolicituds { get; set; }
    }
}
