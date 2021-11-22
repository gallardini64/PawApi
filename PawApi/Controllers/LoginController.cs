using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PawApi.Controllers.DTOs;
using PawApi.Models;
using PawApi.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PawApi.Controllers
{
    [EnableCors()]
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly PawHouseContext context;
        private readonly IConfiguration configuration;

        public LoginController(IConfiguration configuration,PawHouseContext context)
        {
            this.context = context;
            this.configuration = configuration;
        }

        [HttpPost("")]
        public async Task<ActionResult<TokenDto>> logIn([FromBody] LoginDto login)
        {
            var usuarios = RepositorioUsuario.GetAll();
            var usuario = usuarios.Find(x => x.Usuario1 == login.User && x.Contraseña == login.Password);
            if (usuario != null)
            {
                return await Generartoken(login);
            }
            else
            {
                return BadRequest("problemas al iniciar sesion");
            }
        }

        private async Task<TokenDto> Generartoken(LoginDto login)
        {
            var usuario = context.Usuarios.Where(x => x.Usuario1.ToLower() == login.User.ToLower()).FirstOrDefault();
            var claims = new List<Claim>()
            {
                new Claim("user",login.User),
                new Claim("userId",usuario.Id.ToString())
            };
            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llavejwt"]));
            var credenciales = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);
            var expiracion = DateTime.UtcNow.AddHours(2);
            var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiracion, signingCredentials: credenciales);
            return new TokenDto()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expriacion = expiracion
            };
        }

        [HttpGet("")]
        public async Task<ActionResult<List<UsuarioDto>>> GetAllUsers()
        {
            try
            {
                var usuarios = RepositorioUsuario.GetAll();
                FillUsuarios(usuarios);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddCollectionMappers();
                    cfg.CreateMap<Usuario, UsuarioDto>().EqualityComparison((u, udto) => u.Id == udto.Id);
                    cfg.CreateMap<Persona, PersonaDto>().EqualityComparison((u, udto) => u.Id == udto.Id);
                });
                usuarios = usuarios.Where(x => x.EstadoId == 1).ToList();
                var mapper = new Mapper(config);
                return Ok(new Result { Results = mapper.Map<List<UsuarioDto>>(usuarios) } );
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("User")]
        public async Task<ActionResult<List<UsuarioDto>>> CreateUser(CreacionUsuarioDto usuario)
        {
            try
            {
                var usuarioNueno = new Usuario();
                usuarioNueno.Usuario1 = usuario.Usuario;
                usuarioNueno.Contraseña = usuario.Contraseña;
                usuarioNueno.Correo = usuario.Correo;
                usuarioNueno.FechaDeCreacion = DateTime.Today;
                usuarioNueno.EstadoId = 1;
                var persona = new Persona();
                persona.NompreApellido = usuario.NombreYApellido;
                usuarioNueno.Personas.Add(persona);
                context.Usuarios.Add(usuarioNueno);
                var result = await context.SaveChangesAsync();
                return result > 0? Ok("Usuario Registrado con Exito") : BadRequest("Error al crear el usuario");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        private void FillUsuarios(List<Usuario> usuarios)
        {
            foreach (var usuario in usuarios)
            {
                usuario.Personas = context.Personas.Where(x => x.UsuarioId == usuario.Id).ToList();
            }
        }
    }
}
