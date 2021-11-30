using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    [EnableCors()]
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
                var Solicitudes = context.Solicituds.Where(x => x.Estado != "Aprobado" && x.Estado != "Rechazado").ToList();
                FillSolicitudes(Solicitudes);
                MapperConfiguration config = getMapperConfig();
                var mapper = new Mapper(config);
                return Ok(new Result { Results = mapper.Map<List<SolicitudDto>>(Solicitudes) });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        

        [HttpGet("")]
        public async Task<ActionResult<Result>> GetById(int id)
        {
            try
            {
                var Solicitud = context.Solicituds.Find(id);
                if (Solicitud == null)
                {
                    return NotFound("Solicitud inexistente");
                }
                FillSolicitud(Solicitud);
                MapperConfiguration config = getMapperConfig();
                var mapper = new Mapper(config);
                return Ok(new Result { Results = mapper.Map<SolicitudDto>(Solicitud)});
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private void FillSolicitud(Solicitud solicitud)
        {
            solicitud.Usuario = context.Usuarios.Where(x => x.Id == solicitud.UsuarioId).FirstOrDefault();
            solicitud.Cuidador = context.Cuidadors.Where(x => x.Id == solicitud.CuidadorId).FirstOrDefault();
            solicitud.FotoSolicituds = context.FotoSolicituds.Where(x => x.SolicitudId == solicitud.Id).ToList();
            FillFotoSolicituds(solicitud.FotoSolicituds);
            FillUsuario(solicitud.Usuario);
            FillCuidadorHogares(solicitud.Cuidador);
        }

        private void FillFotoSolicituds(ICollection<FotoSolicitud> fotoSolicituds)
        {
            foreach (var solfoto in fotoSolicituds)
            {
                FillFotos(solfoto);
            }
        }

        private void FillFotos(FotoSolicitud solfoto)
        {
            solfoto.Foto = context.Fotos.Where(x => x.Id == solfoto.FotoId).FirstOrDefault();
        }

        private void FillSolicitudes(List<Solicitud> solicitudes)
        {
            foreach (var solicitud in solicitudes)
            {
                FillSolicitud(solicitud);
            }
        }
        private void FillCuidadorHogares(Cuidador cuidador)
        {
            cuidador.HogarTemporals = context.HogarTemporals.Where(x => x.CuidadorId == cuidador.Id).ToList();
        }

        private void FillUsuario(Usuario usuario)
        {
            usuario.Personas = context.Personas.Where(x => x.UsuarioId == usuario.Id).ToList();
        }

        [HttpPost("")]
        public async Task<ActionResult> Post([FromBody] SolicitudDto solicitudDto)
        {
            try
            {
                MapperConfiguration config = getMapperConfig();
                var mapper = new Mapper(config);
                var solicitud = mapper.Map<Solicitud>(solicitudDto);
                if (solicitud.Estado.ToLower() == "rechazado")
                {
                    solicitud.Cuidador.Estado = "Inactivo";
                }
                context.Entry(solicitud).State = EntityState.Modified;
                return context.SaveChanges() > 0 ?  Ok("solicitud modificada con exito") : BadRequest("error al modificar solicitud");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] SolicitudDto solicitudDto)
        {
            try
            {
                MapperConfiguration config = getMapperConfig();
                var mapper = new Mapper(config);
                var soli = new Solicitud()
                {
                    
                    DniSolicitud = solicitudDto.DniSolicitud,
                    Estado = solicitudDto.Estado,
                    FechaSolicitud = DateTime.Today,
                    FotoSolicituds = mapper.Map<List<FotoSolicitud>>(solicitudDto.FotoSolicituds),
                    UsuarioId = solicitudDto.UsuarioId,
                    Peticion = solicitudDto.Peticion,
                    Cuidador = new Cuidador { Telefono = solicitudDto.cuidador.Telefono , Puntaje = 0, FotoPerfil = solicitudDto.cuidador.FotoPerfil,
                                            UsuarioId = solicitudDto.cuidador.UsuarioId,
                                            HogarTemporals = new List<HogarTemporal>() { new HogarTemporal() {
                                                                                        Direccion = solicitudDto.cuidador.HogarTemporals.First().Direccion,
                                                                                        Localidad = solicitudDto.cuidador.HogarTemporals.First().Localidad,
                                                                                        Provincia = solicitudDto.cuidador.HogarTemporals.First().Provincia,
                                                                                        TipoHogarId = solicitudDto.cuidador.HogarTemporals.First().TipoHogarId
                                                                                        } },
                                            CuidadorMascota = new List<CuidadorMascota>() { new CuidadorMascota {
                                                                                       Cantidad = solicitudDto.cuidador.CuidadorMascota.First().Cantidad,
                                                                                       EspecieId = solicitudDto.cuidador.CuidadorMascota.First().EspecieId,
                                                                                       PrecioUnitarioDiario = solicitudDto.cuidador.CuidadorMascota.First().PrecioUnitarioDiario,
                                            } },
                                            DescripcionServicio = solicitudDto.cuidador.DescripcionServicio
                    }
                };
                //var solicitud = mapper.Map<Solicitud>(solicitudDto);
                context.Solicituds.Add(soli);
                return context.SaveChanges() > 0 ? Ok("solicitud creada con exito") : BadRequest("error al crear solicitud");
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
                cfg.CreateMap<Cuidador, CuidadorDto>().EqualityComparison((u, udto) => u.Id == udto.Id).ReverseMap();
                cfg.CreateMap<FotoSolicitud, FotoSolicitudDto>().EqualityComparison((u, udto) => u.Id == udto.Id).ReverseMap();
                cfg.CreateMap<Foto, FotoDto>().EqualityComparison((u, udto) => u.Id == udto.Id).ReverseMap();
                cfg.CreateMap<Usuario, UsuarioDto>().EqualityComparison((u, udto) => u.Id == udto.Id).ReverseMap();
                cfg.CreateMap<Persona, PersonaDto>().EqualityComparison((u, udto) => u.Id == udto.Id).ReverseMap();
                cfg.CreateMap<HogarTemporal, HogarTemporalDto>().EqualityComparison((u, udto) => u.Id == udto.Id).ReverseMap();
                cfg.CreateMap<Resenium, ReseniumDto>().EqualityComparison((u, udto) => u.Id == udto.Id).ReverseMap();
                cfg.CreateMap<CuidadorMascota, CuidadorMascotaDto>().EqualityComparison((u, udto) => u.Id == udto.Id).ReverseMap();
                cfg.CreateMap<FotoCuidador, FotoCuidadorDto>().EqualityComparison((u, udto) => u.Id == udto.Id).ReverseMap();
            });
        }
    }
}
