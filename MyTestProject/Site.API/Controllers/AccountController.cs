using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyTestProject.Helpers;
using MyTestProject.Models;
using MyTestProject.Options;
using Newtonsoft.Json;
using Site.API.BindingModels;
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
        public async Task<IActionResult> LoginAsync([FromBody] LoginBindingModel model)
        {
            var user = await _context.UserManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Not found");
            }
            else
            {
                var claims = new List<Claim> {
                    new Claim( ClaimsIdentity.DefaultNameClaimType, user.UserName),
                    new Claim( JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString() )
                };
                var creds = new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _options.Value.Issuer,
                    audience: _options.Value.Issuer,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: creds);


                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), name = user.Name });
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] RegisterBindingModel model)
        {

            var user = new User { Name = model.Name};
            _context.Add(user);
            _context.SaveChanges();
            var claims = new[]
            {
                    new Claim(ClaimsIdentity.DefaultNameClaimType,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
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
