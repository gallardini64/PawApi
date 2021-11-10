using System;
using System.Collections.Generic;

#nullable disable

namespace PawApi.Models
{
    public partial class Resenium : IEntityBase
    {
        public int Id { get; set; }
        public string Resenia { get; set; }
        public decimal? Puntuacion { get; set; }
        public int? CuidadorId { get; set; }
        public int? EstadiaId { get; set; }
        public virtual Cuidador Cuidador { get; set; }
    }
}
