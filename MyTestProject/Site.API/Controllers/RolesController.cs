using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTestProject.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Site.API.Controllers
{
  //[Authorize(Roles = "Admin")]
  [AllowAnonymous]
  [Route("api/[controller]")]
  public class RolesController : Controller
  {
    private readonly DatabaseContext _context;

    public RolesController(DatabaseContext context)
    {
      _context = context;
    }

    
    [HttpPost]
    public async Task<IActionResult> AddUserToRoleAsync(string userId, int roleId) //arguments for swagger ui
    {
      //var userId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value; // Get user id from token Sid claim
      var user = await _context.UserManager.FindByIdAsync(userId);
      //if (user == null)
      //{
      //  return Forbid("User not found!");
      //}
      var userRole = await _context.UserManager.AddToRoleAsync(user, "Admin");

      return Ok("Success");
    }
  }
}
