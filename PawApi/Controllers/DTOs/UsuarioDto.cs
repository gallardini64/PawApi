using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PawApi.Controllers.DTOs
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Usuario1 { get; set; }
        public string Correo { get; set; }
        public string Legajo { get; set; }
        public DateTime? FechaDeCreacion { get; set; }
        public int? EstadoId { get; set; }
        public IEnumerable<PersonaDto> Personas { get; set; }

    }
}
