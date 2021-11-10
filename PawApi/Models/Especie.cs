using System;
using System.Collections.Generic;

#nullable disable

namespace PawApi.Models
{
    public partial class Especie : IEntityBase
    {
        public Especie()
        {
            CuidadorMascota = new HashSet<CuidadorMascota>();
        }

        public int Id { get; set; }
        public string Especie1 { get; set; }

        public virtual ICollection<CuidadorMascota> CuidadorMascota { get; set; }
    }
}
