using System;
using System.Collections.Generic;

#nullable disable

namespace PawApi.Models
{
    public partial class TipoHogar : IEntityBase
    {
        public TipoHogar()
        {
            HogarTemporals = new HashSet<HogarTemporal>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<HogarTemporal> HogarTemporals { get; set; }
    }
}
