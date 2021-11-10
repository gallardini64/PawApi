using System;
using System.Collections.Generic;

#nullable disable

namespace PawApi.Models
{
    public partial class Denuncium : IEntityBase
    {
        public int Id { get; set; }
        public string Denuncia { get; set; }
        public int? CuidadorId { get; set; }

        public virtual Cuidador Cuidador { get; set; }
    }
}
