using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PawApi.Controllers.DTOs;
using PawApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PawApi.Controllers
{
    [EnableCors()]
    [Route("api/Comodidad")]
    [ApiController]
    public class ComodidadController : ControllerBase
    {
        private PawHouseContext context;
        public ComodidadController(PawHouseContext context)
        {
            this.context = context;
        }
        [HttpGet("")]
        public async Task<ActionResult<List<ComodidadDto>>> GetAll()
        {
            try
            {
                var comodidades = context.Comodidads.ToList();
                MapperConfiguration config = GetMapperConfig();
                var mapper = new Mapper(config);
                return Ok(new Result { Results = mapper.Map<List<ComodidadDto>>(comodidades) });
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
                cfg.CreateMap<Comodidad, ComodidadDto>().EqualityComparison((u, udto) => u.Id == udto.Id);
            });
        }
    }
}
