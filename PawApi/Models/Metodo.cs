using System;
using System.Collections.Generic;

#nullable disable

namespace PawApi.Models
{
    public partial class Metodo : IEntityBase
    {
        public Metodo()
        {
            Pagos = new HashSet<Pago>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Pago> Pagos { get; set; }
    }
}
