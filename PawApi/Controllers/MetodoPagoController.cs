using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Microsoft.AspNetCore.Mvc;
using PawApi.Controllers.DTOs;
using PawApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PawApi.Controllers
{
    [Route("api/MetodoPago")]
    [ApiController]
    public class MetodoPagoController : ControllerBase
    {
        private PawHouseContext context;
        public MetodoPagoController(PawHouseContext cont)
        {
            this.context = cont;
        }
        [HttpGet("")]
        public async Task<ActionResult<List<MetodoDto>>> getMetodos()
        {
            var metodos = context.Metodos.ToList();
            MapperConfiguration config = getMapperConfig();
            var mapper = new Mapper(config);
            return Ok(new Result { Results = mapper.Map<List<MetodoDto>>(metodos) });
        }
        private static MapperConfiguration getMapperConfig()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddCollectionMappers();
                cfg.CreateMap<Solicitud, SolicitudDto>().EqualityComparison((u, udto) => u.Id == udto.Id).ReverseMap();
                cfg.CreateMap<FotoSolicitud, FotoSolicitudDto>().EqualityComparison((u, udto) => u.Id == udto.Id).ReverseMap();
                cfg.CreateMap<Foto, FotoDto>().EqualityComparison((u, udto) => u.Id == udto.Id).ReverseMap();
                cfg.CreateMap<Metodo, MetodoDto>().EqualityComparison((u, udto) => u.Id == udto.Id).ReverseMap();
            });
        }
    }
}
