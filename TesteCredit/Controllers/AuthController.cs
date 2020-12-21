using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TesteCredit.Domains.Entities.Authentication;
using TesteCredit.Domains.Repositories;
using TesteCredit.Domains.Repositories.Authentication;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TesteCredit.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthenticationRepository authenticationRepository, IConfiguration configuration)
        {
            _authenticationRepository = authenticationRepository;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public IActionResult PostLogin([FromBody] User user)
        {
            if(user.UserName == null || user.PassWordHash == null)
                return BadRequest(new { message = "Favor informar a senha" });

            string active = _authenticationRepository.GetSignInAsync(user.UserName, user.PassWordHash).Result;

            if (active == "Y")
            {
                return Ok(BuildToken(user));
            }
            else
            {
                return BadRequest(new { message = "Não é possivel efetuar o login" });
            }
        }

        private UserToken BuildToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Email, user.UserName),
            new Claim("DateOfJoing", DateTime.Now.ToString("yyyy-MM-dd")),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = DateTime.Now.AddMinutes(120)
            };
        }
    }
}
