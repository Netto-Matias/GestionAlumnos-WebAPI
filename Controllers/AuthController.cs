using Alumnos.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Alumnos.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        // Inyectamos IConfiguration para poder leer la clave secreta del appsettings.json
        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO loginDto)
        {
            // Simulo usuario unico
            if (loginDto.Usuario == "admin" && loginDto.Password == "admin123")
            {
                // 1. Creacion de Credenciales
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                // 2. Fabricacion del Token JWT con sus datos de emisor y vencimiento (1 hora)
                var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
                );

                // 3. Conversion de token en texto largo y encriptado
                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new { token = jwtToken });
            }

            // Devolucion de código 401 si los datos estan mal
            return Unauthorized("Usuario o contraseña incorrectos.");
        }
    }
}

