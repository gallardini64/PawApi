using System;
using System.Collections.Generic;

#nullable disable

namespace PawApi.Models
{
    public partial class FotoCuidador : IEntityBase
    {
        public int Id { get; set; }
        public int? FotoId { get; set; }
        public int? CuidadorId { get; set; }

        public virtual Cuidador Cuidador { get; set; }
        public virtual Foto Foto { get; set; }
    }
}
