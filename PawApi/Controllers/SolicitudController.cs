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
    [Route("api/Solicitud")]
    [ApiController]
    public class SolicitudController : ControllerBase
    {
        private PawHouseContext context;
        public SolicitudController(PawHouseContext cont)
        {
            this.context = cont;
        }

        [HttpGet("List")]
        public async Task<ActionResult<List<SolicitudDto>>> GetAllRequest()
        {
            try
            {
                var Solicitudes = context.Solicituds.ToList();
                MapperConfiguration config = getMapperConfig();
                var mapper = new Mapper(config);
                return Ok(mapper.Map<List<SolicitudDto>>(Solicitudes));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("")]
        public async Task<ActionResult<SolicitudDto>> GetById(int id)
        {
            try
            {
                var Solicitud = context.Solicituds.Find(id);
                if (Solicitud == null)
                {
                    return NotFound("Solicitud inexistente");
                }
                MapperConfiguration config = getMapperConfig();
                var mapper = new Mapper(config);
                return Ok(mapper.Map<SolicitudDto>(Solicitud));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("")]
        public async Task<ActionResult> Post([FromBody] SolicitudDto solicitudDto)
        {
            try
            {
                MapperConfiguration config = getMapperConfig();
                var mapper = new Mapper(config);
                var solicitud = mapper.Map<Solicitud>(solicitudDto);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private static MapperConfiguration getMapperConfig()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddCollectionMappers();
                cfg.CreateMap<Solicitud, SolicitudDto>().EqualityComparison((u, udto) => u.Id == udto.Id).ReverseMap();
                cfg.CreateMap<FotoSolicitud, FotoSolicitudDto>().EqualityComparison((u, udto) => u.Id == udto.Id).ReverseMap();
                cfg.CreateMap<Foto, FotoDto>().EqualityComparison((u, udto) => u.Id == udto.Id).ReverseMap();

            });
        }
    }
}
