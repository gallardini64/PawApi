using AutoMapper;
using AutoMapper.EquivalencyExpression;
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
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public LoginController(IConfiguration configuration)
        {
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
            var claims = new List<Claim>()
            {
                new Claim("user",login.User)
            };
            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llavejwt"]));
            var credenciales = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);
            var expiracion = DateTime.UtcNow.AddMinutes(20);
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
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddCollectionMappers();
                    cfg.CreateMap<Usuario, UsuarioDto>().EqualityComparison((u, udto) => u.Id == udto.Id);
                });
                var mapper = new Mapper(config);
                return Ok(mapper.Map<List<UsuarioDto>>(usuarios));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
