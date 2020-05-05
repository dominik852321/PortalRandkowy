using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PortalRandkowy.API.Data;
using PortalRandkowy.API.Dtos;
using PortalRandkowy.API.Interfaces;
using PortalRandkowy.API.Model;

namespace PortalRandkowy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly DataContext _dataContext;
        public readonly IAuthRepository _repositoryAuth;
        public readonly IConfiguration _config ;

        public AuthController(IAuthRepository repository, DataContext dataContext,
                              IConfiguration config)
        {
            _dataContext= dataContext;
            _config = config;
            _repositoryAuth = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> Get()
               => await _dataContext.Users.ToListAsync();


        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO userForRegister)
        {
            userForRegister.Username = userForRegister.Username.ToLower();

            if(await _repositoryAuth.UserExists(userForRegister.Username))
                 return BadRequest("Użytkownik o takiej nazwie już istnieje");
            
            var user = new User{ UserName = userForRegister.Username};
           
            var createdUser = await _repositoryAuth.Register(user, userForRegister.Password);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDTO userForLogin)
        {
            var userFromRepo = await _repositoryAuth
                            .Login(userForLogin.Username.ToLower(), userForLogin.Password);

            if(userFromRepo == null)
                 return Unauthorized();     

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Userid.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(5),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new { token = tokenHandler.WriteToken(token)});
        }
        
    }
}