using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MyTestProject.Models;

namespace Site.API.Controllers
{
  //[Authorize(Roles = "Admin")]
  [Authorize]
  [Route("api/[controller]")]
  public class RolesController : Controller
  {
    private readonly UserManager<User> _userManager;

    public RolesController(UserManager<User> userManager)
    {
      _userManager = userManager;
    }
    [HttpPost]
    [Route("add")]
    private async Task AddToRole()
    {
      var userId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value; // Get user id from token Sid claim
      var user = await _userManager.FindByIdAsync(userId);
      //here we tie the new user to the "Member" role 
      await _userManager.AddToRoleAsync(user, "Member");
    }
  }
}
