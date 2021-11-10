using System;
using System.Collections.Generic;

#nullable disable

namespace PawApi.Models
{
    public partial class Comodidad : IEntityBase
    {
        public Comodidad()
        {
            HogarDetalles = new HashSet<HogarDetalle>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<HogarDetalle> HogarDetalles { get; set; }
    }
}
