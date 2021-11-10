using System;
using System.Collections.Generic;

#nullable disable

namespace PawApi.Models
{
    public partial class UsuarioRol : IEntityBase
    {
        public int Id { get; set; }
        public int? RolId { get; set; }
        public int? UsuarioId { get; set; }

        public virtual Rol Rol { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
