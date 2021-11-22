using System;
using System.Collections.Generic;

#nullable disable

namespace PawApi.Models
{
    public partial class Cuidador : IEntityBase
    {
        public Cuidador()
        {
            CuidadorMascota = new HashSet<CuidadorMascota>();
            Denuncia = new HashSet<Denuncium>();
            Estadia = new HashSet<Estadium>();
            FotoCuidadors = new HashSet<FotoCuidador>();
            HogarTemporals = new HashSet<HogarTemporal>();
            Resenia = new HashSet<Resenium>();
            Solicituds = new HashSet<Solicitud>();
        }

        public int Id { get; set; }
        public int? UsuarioId { get; set; }
        public string Telefono { get; set; }
        public decimal? Puntaje { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<CuidadorMascota> CuidadorMascota { get; set; }
        public virtual ICollection<Denuncium> Denuncia { get; set; }
        public virtual ICollection<Estadium> Estadia { get; set; }
        public virtual ICollection<FotoCuidador> FotoCuidadors { get; set; }
        public virtual ICollection<HogarTemporal> HogarTemporals { get; set; }
        public virtual ICollection<Resenium> Resenia { get; set; }
        public virtual ICollection<Solicitud> Solicituds { get; set; }
        public string FotoPerfil { get; set; }
        public string DescripcionServicio { get; set; }
    }
}
