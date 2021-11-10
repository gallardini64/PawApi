using System;
using System.Collections.Generic;

#nullable disable

namespace PawApi.Models
{
    public partial class HogarTemporal : IEntityBase
    {
        public HogarTemporal()
        {
            Estadia = new HashSet<Estadium>();
            HogarDetalles = new HashSet<HogarDetalle>();
        }

        public int Id { get; set; }
        public string Direccion { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }
        public int? CuidadorId { get; set; }
        public int? TipoHogarId { get; set; }

        public virtual Cuidador Cuidador { get; set; }
        public virtual TipoHogar TipoHogar { get; set; }
        public virtual ICollection<Estadium> Estadia { get; set; }
        public virtual ICollection<HogarDetalle> HogarDetalles { get; set; }
    }
}
