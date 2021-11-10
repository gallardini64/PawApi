using System;
using System.Collections.Generic;

#nullable disable

namespace PawApi.Models
{
    public partial class HogarDetalle : IEntityBase
    {
        public int Id { get; set; }
        public int? HogarId { get; set; }
        public int? ComodidadId { get; set; }

        public virtual Comodidad Comodidad { get; set; }
        public virtual HogarTemporal Hogar { get; set; }
    }
}
