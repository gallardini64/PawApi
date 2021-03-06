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
        public async Task<ActionResult<Result>> GetAll()
        {
            try
            {
                var cuidadores = context.Cuidadors.Where(x => x.Estado.ToLower() == "activo").ToList();
                FillCuidadores(cuidadores);
                MapperConfiguration config = GetMapperConfig();
                var mapper = new Mapper(config);
                return Ok(new Result() { Results = mapper.Map<List<CuidadorDto>>(cuidadores) });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("")]
        public async Task<ActionResult> NewEstadia([FromBody] EstadiaDto contrato)
        {
            try
            {
                var config = GetMapperConfig();
                var mapper = new Mapper(config);
                var estadia = mapper.Map<Estadium>(contrato);
                context.Estadia.Add(estadia);
                var result = await context.SaveChangesAsync();
                return result > 0 ? Ok("Estadia Guardada") : BadRequest("Error al guardar");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("EstadiasByPersona")]
        public async Task<ActionResult> GetEstadias(int personaID)
        {
            try
            {
                var Estadias = context.Estadia.Where(x => x.PersonaId == personaID).ToList();
                var config = GetMapperConfig();
                var mapper = new Mapper(config);
                return Ok(new Result { Results = mapper.Map<List<EstadiaDto>>(Estadias) });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        private void FillCuidadores(List<Cuidador> cuidadores)
        {
            foreach (var cuidador in cuidadores)
            {
                cuidador.HogarTemporals.Add(context.HogarTemporals.Where(x => x.CuidadorId == cuidador.Id).FirstOrDefault());
                cuidador.Resenia = context.Resenia.Where(x => x.CuidadorId == cuidador.Id).ToList();
                cuidador.CuidadorMascota = context.CuidadorMascotas.Where(x => x.CuidadorId == cuidador.Id).ToList();
                cuidador.FotoCuidadors = context.FotoCuidadors.Where(x => x.CuidadorId == cuidador.Id).ToList();
                FillFotos(cuidador.FotoCuidadors.ToList());
            }
        }

        private void FillFotos(List<FotoCuidador> lists)
        {
            foreach (var foto in lists)
            {
                foto.Foto = context.Fotos.Where(x => x.Id == foto.FotoId).FirstOrDefault();
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
                cfg.CreateMap<HogarTemporal, HogarTemporalDto>().EqualityComparison((u, udto) => u.Id == udto.Id);
                cfg.CreateMap<FotoCuidador, FotoCuidadorDto>().EqualityComparison((u, udto) => u.Id == udto.Id);
                cfg.CreateMap<Foto, FotoDto>().EqualityComparison((u, udto) => u.Id == udto.Id);
                cfg.CreateMap<Pago, PagoDto>().EqualityComparison((u, udto) => u.Id == udto.Id).ReverseMap();
                cfg.CreateMap<Persona, PersonaDto>().EqualityComparison((u, udto) => u.Id == udto.Id).ReverseMap();
                cfg.CreateMap<Estadium, EstadiaDto>().ReverseMap();
            });
        }
    }
}
