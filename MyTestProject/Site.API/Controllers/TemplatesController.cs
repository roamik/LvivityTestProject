using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Site.API.BindingModels;
using Site.API.DAL.Abstract;
using Site.API.DAL.Concrete;
using Site.API.Models;

namespace Site.API.Controllers
{
  
  [Route("api/[controller]")]
  [Authorize(Roles = "Admin, Member")]
  public class TemplatesController : Controller
  {
    private readonly ITemplatesRepository _templateRep;

    public TemplatesController(ITemplatesRepository templateRep)
    {
      _templateRep = templateRep;
    }

    [HttpGet]
    [Route("my")]
    public async Task<IActionResult> GetMyTemplates()
    {
      var userId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value;
      var templates = await _templateRep.GetFilteredByUserIdAsync(userId);
      return Ok(templates);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTemplateAsync([FromBody] AddTemplateBindingModel model)
    {
      var userId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value; // Get user id from token Sid claim
      var template = new Template { Content = model.Content, Decription = model.Description, Name = model.Name, UserId = userId };
      await _templateRep.AddAsync(template);
      await _templateRep.Save();
      return Ok(template);
    }
  }
}
