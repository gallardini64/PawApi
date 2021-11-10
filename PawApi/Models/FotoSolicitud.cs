using System;
using System.Collections.Generic;

#nullable disable

namespace PawApi.Models
{
    public partial class FotoSolicitud : IEntityBase
    {
        public int Id { get; set; }
        public int? FotoId { get; set; }
        public int? SolicitudId { get; set; }

        public virtual Foto Foto { get; set; }
        public virtual Solicitud Solicitud { get; set; }
    }
}
