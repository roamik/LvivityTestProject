using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyTestProject.Models;
using MyTestProject.Options;
using Newtonsoft.Json;
using Site.API.BindingModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyTestProject.Controllers
{
  [Route("api/[controller]")]
  [AllowAnonymous]
  public class AccountController : Controller
  {
    private readonly DatabaseContext _context;
    private readonly IOptions<JWTOptions> _options;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AccountController(DatabaseContext context, IOptions<JWTOptions> options, UserManager<User> userManager, SignInManager<User> signInManager)
    {
      _context = context;
      _options = options;
      _userManager = userManager;
      _signInManager = signInManager;
    }

    // POST api/account/login
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginBindingModel model)
    {
      var user = await _context.UserManager.FindByEmailAsync(model.Email);
      if (user == null)
      {
        ModelState.AddModelError("Email", "Not found");
      }
      else
      {
        var roles = await _context.UserManager.GetRolesAsync(user);
        var claims = new List<Claim> {
                    new Claim( ClaimsIdentity.DefaultNameClaimType, user.UserName),
                    new Claim( JwtRegisteredClaimNames.Sid, user.Id ),
                    new Claim( JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sid, user.Id)
                };
        if (roles.Any())
        {
          claims.AddRange(roles.Select(role => new Claim(JwtRegisteredClaimNames.Sub, role)));
        }
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

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
    public async Task<IActionResult> Register([FromBody] RegisterBindingModel model)
    {



      var user = new User { Name = model.Name, UserName = model.Email, Email = model.Email };
      var result = await _userManager.CreateAsync(user, model.Password);


      if (!result.Succeeded) { return BadRequest("Could not create token"); }
      var claims = new[]
      {
                    new Claim(ClaimsIdentity.DefaultNameClaimType,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sid, user.Id) // Set userid to token Sid claim
                };
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Key));
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      var token = new JwtSecurityToken(_options.Value.Issuer,
          _options.Value.Issuer,
          claims,
          expires: DateTime.Now.AddMinutes(30),
          signingCredentials: creds);

      return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), id = user.Id });
    }
  }
}
