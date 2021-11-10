using System;
using System.Collections.Generic;

#nullable disable

namespace PawApi.Models
{
    public partial class Usuario : IEntityBase
    {
        public Usuario()
        {
            Cuidadors = new HashSet<Cuidador>();
            Personas = new HashSet<Persona>();
            UsuarioRols = new HashSet<UsuarioRol>();
        }

        public int Id { get; set; }
        public string Usuario1 { get; set; }
        public string Contraseña { get; set; }
        public string Correo { get; set; }
        public string Legajo { get; set; }
        public DateTime? FechaDeCreacion { get; set; }
        public int? EstadoId { get; set; }

        public virtual Estado Estado { get; set; }
        public virtual ICollection<Cuidador> Cuidadors { get; set; }
        public virtual ICollection<Persona> Personas { get; set; }
        public virtual ICollection<UsuarioRol> UsuarioRols { get; set; }
    }
}
