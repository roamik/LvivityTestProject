using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyTestProject.Helpers;
using MyTestProject.Models;
using MyTestProject.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyTestProject.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IOptions<JWTOptions> _options;

        public AccountController(DatabaseContext context, IOptions<JWTOptions> options)
        {
            _context = context;
            _options = options;
        }

        // POST api/account/login
        [HttpPost]
        [Route("login")]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.First(x => x.Login == username && x.Password == password);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Not found");
            }
            else
            {
                var claims = new List<Claim> {
                    new Claim( ClaimsIdentity.DefaultNameClaimType, user.Login ),
                    new Claim( ClaimsIdentity.DefaultRoleClaimType, user.Role ),
                    new Claim( JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString() )
                };
                var creds = new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _options.Value.Issuer,
                    audience: _options.Value.Issuer,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: creds);


                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), username = user.Login });
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(string username, string password, string role)
        {

            var user = new User { Login = username, Password = password, Role = role };
            _context.Add(user);
            _context.SaveChanges();
            var claims = new[]
            {
                    new Claim(ClaimsIdentity.DefaultNameClaimType,user.Login),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Login),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

            var creds = new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_options.Value.Issuer,
                _options.Value.Issuer,
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), id = user.Id });
        }
    }
}
