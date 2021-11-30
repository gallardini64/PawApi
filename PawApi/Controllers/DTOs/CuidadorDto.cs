using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PawApi.Controllers.DTOs
{
    public class CuidadorDto
    {
        public int Id { get; set; }
        public string Telefono { get; set; }
        public decimal Puntuaje { get; set; }
        public string FotoPerfil { get; set; }
        public string DescripcionServicio { get; set; }
        public int? UsuarioId { get; set; }
        public IEnumerable<HogarTemporalDto> HogarTemporals { get; set; }
        public IEnumerable<ReseniumDto> Resenia { get; set; }
        public IEnumerable<CuidadorMascotaDto> CuidadorMascota { get; set; }
        public IEnumerable<FotoCuidadorDto> FotoCuidadors { get; set; }
        public string Estado { get; set; }

    }
}
