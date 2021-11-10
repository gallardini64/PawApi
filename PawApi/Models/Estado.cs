using System;
using System.Collections.Generic;

#nullable disable

namespace PawApi.Models
{
    public partial class Estado : IEntityBase
    {
        public Estado()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        public string Estado1 { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
