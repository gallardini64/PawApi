using System;
using System.Collections.Generic;

#nullable disable

namespace PawApi.Models
{
    public partial class Pago : IEntityBase
    {
        public int Id { get; set; }
        public int? EstadiaId { get; set; }
        public DateTime? FechaEmitido { get; set; }
        public DateTime? FechaPagado { get; set; }
        public decimal? Total { get; set; }
        public decimal? Importe { get; set; }
        public decimal? Comision { get; set; }
        public string NumeroRefencia { get; set; }
        public string Estado { get; set; }
        public int? MetodoId { get; set; }

        public virtual Estadium Estadia { get; set; }
        public virtual Metodo Metodo { get; set; }
    }
}
