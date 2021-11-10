using System;
using System.Collections.Generic;

#nullable disable

namespace PawApi.Models
{
    public partial class Foto : IEntityBase
    {
        public Foto()
        {
            FotoCuidadors = new HashSet<FotoCuidador>();
            FotoSolicituds = new HashSet<FotoSolicitud>();
        }

        public int Id { get; set; }
        public string Path { get; set; }

        public virtual ICollection<FotoCuidador> FotoCuidadors { get; set; }
        public virtual ICollection<FotoSolicitud> FotoSolicituds { get; set; }
    }
}
