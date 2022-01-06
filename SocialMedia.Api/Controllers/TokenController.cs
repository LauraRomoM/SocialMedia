
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Core.CustomEntities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Authentication(UserLogin login)
        {
            //si es usuario valido
            if(IsValidUser(login))
            {
                var token = GenerateToken();
                return Ok(new { token });
            }
            return NotFound();
        }

        private bool IsValidUser(UserLogin login)
        {
            return true;
        }

        private string GenerateToken()
        {
            //Header
            var _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var SigningCredential = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(SigningCredential);

            //Claims        (los claims son necesarios para crear los payloado)
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "Laura Romo M"),             //los claim, son pares de datos
                new Claim(ClaimTypes.Email, "LRMunoz@gmail.com"),
                new Claim(ClaimTypes.Role, "Director"),
            };

            //Pay Load
            var payload = new JwtPayload
            (
                _configuration["Authentication:Issuer"],           //Iss (emisor)
                _configuration["Authentication:Audience"],
                claims,                                         //los claim, son pares de datos
                DateTime.Now,                             //Tiempo en que inicia token 
                DateTime.UtcNow.AddMinutes(50)              //Tiempo en que vence el Token
            );

            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);         //Genera o serializa el token a un tipo JWT
        }

    }
}
