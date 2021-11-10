using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PawApi.Controllers.DTOs
{
    public class TokenDto
    {
        public string Token { get; set; }
        public DateTime Expriacion { get; set; }
    }
}
