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
    [Route("api/Especies")]
    [ApiController]
    public class EspecieController : ControllerBase
    {
        private PawHouseContext context;
        public EspecieController(PawHouseContext context)
        {
            this.context = context;
        }

        [HttpGet("")]
        public async Task<ActionResult<Result>> GetAll()
        {
            try
            {
                var especies = context.Especies.ToList();
                MapperConfiguration config = GetMapperConfig();
                var mapper = new Mapper(config);
                return Ok(new Result {Results = mapper.Map<List<EspecieDto>>(especies)});
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
                cfg.CreateMap<Especie, EspecieDto>().EqualityComparison((u, udto) => u.Id == udto.Id);
            });
        }
    }
}
