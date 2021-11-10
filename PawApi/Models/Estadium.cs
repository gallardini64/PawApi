using System;
using System.Collections.Generic;

#nullable disable

namespace PawApi.Models
{
    public partial class Estadium : IEntityBase
    {
        public Estadium()
        {
            Pagos = new HashSet<Pago>();
        }

        public int Id { get; set; }
        public int? PersonaId { get; set; }
        public int? CuidadorId { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? CantidadMacotas { get; set; }
        public bool? Confirmada { get; set; }
        public int? HogarId { get; set; }

        public virtual Cuidador Cuidador { get; set; }
        public virtual HogarTemporal Hogar { get; set; }
        public virtual Persona Persona { get; set; }
        public virtual ICollection<Pago> Pagos { get; set; }
    }
}
