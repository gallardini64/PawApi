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
    [Route("api/Cuidador")]
    [ApiController]
    public class CuidadorController : ControllerBase
    {
        private PawHouseContext context;

        public CuidadorController(PawHouseContext context)
        {
            this.context = context;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<CuidadorDto>>> GetAll()
        {
            try
            {
                var cuidadores = context.Cuidadors.ToList();
                MapperConfiguration config = GetMapperConfig();
                var mapper = new Mapper(config);
                return Ok(mapper.Map<List<CuidadorDto>>(cuidadores));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private static MapperConfiguration GetMapperConfig()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddCollectionMappers();
                cfg.CreateMap<Cuidador, CuidadorDto>().EqualityComparison((u, udto) => u.Id == udto.Id);
                cfg.CreateMap<Resenium, ReseniumDto>().EqualityComparison((u, udto) => u.Id == udto.Id);
                cfg.CreateMap<CuidadorMascota, CuidadorMascotaDto>().EqualityComparison((u, udto) => u.Id == udto.Id);
            });
        }

        [HttpPost("")]
        public async Task<ActionResult> GetAllUsers([FromBody] ContratoDto contrato)
        {
            try
            {
                var cuidadores = context.Cuidadors.ToList();
                var config = GetMapperConfig();
                var mapper = new Mapper(config);
                return Ok("No implementado");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
