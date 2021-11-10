using System;
using System.Collections.Generic;

#nullable disable

namespace PawApi.Models
{
    public partial class Persona : IEntityBase
    {
        public Persona()
        {
            Estadia = new HashSet<Estadium>();
        }

        public int Id { get; set; }
        public string NompreApellido { get; set; }
        public long? Dni { get; set; }
        public int? UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<Estadium> Estadia { get; set; }
    }
}
