using System;
using System.Collections.Generic;

#nullable disable

namespace PawApi.Models
{
    public partial class CuidadorMascota : IEntityBase
    {
        public int Id { get; set; }
        public int? CuidadorId { get; set; }
        public int? EspecieId { get; set; }
        public int? Cantidad { get; set; }
        public decimal? PrecioUnitarioDiario { get; set; }

        public virtual Cuidador Cuidador { get; set; }
        public virtual Especie Especie { get; set; }
    }
}
