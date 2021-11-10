using System;
using System.Collections.Generic;

#nullable disable

namespace PawApi.Models
{
    public partial class Rol : IEntityBase
    {
        public Rol()
        {
            UsuarioRols = new HashSet<UsuarioRol>();
        }

        public int Id { get; set; }
        public string Rol1 { get; set; }

        public virtual ICollection<UsuarioRol> UsuarioRols { get; set; }
    }
}
