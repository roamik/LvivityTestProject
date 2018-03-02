using System;
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
  [Authorize(Roles = "Admin,Member")]
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

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetTemplateByIdAsync(Guid id)
    {
      var template = await _templateRep.FirstAsync(id);
      if (template == null)
      {
        return BadRequest("User not found!");
      }

      return Ok(new { Name = template.Name, Description = template.Description, Content = template.Content });
    }

    [HttpPost]
    public async Task<IActionResult> CreateTemplateAsync([FromBody] AddTemplateBindingModel model)
    {
      var userId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value; // Get user id from token Sid claim
      var template = new Template { Content = model.Content, Description = model.Description, Name = model.Name, UserId = userId };
      await _templateRep.AddAsync(template);
      await _templateRep.Save();
      return Ok(template);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
      if (!await _templateRep.ExistAsync(id))
      {
        return NotFound($"Item {id} doesn't exist!");
      }

      var template = await _templateRep.GetByIdAsync(id);

      _templateRep.Delete(template);
      await _templateRep.Save();
      return NoContent();
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> Update([FromBody] UpdateTemplateBindingModel model, Guid id)
    {
      if (!ModelState.IsValid || model == null)
      {
        return BadRequest(ModelState);
      }

      if (!await _templateRep.ExistAsync(id))
      {
        return NotFound($"Item {id} doesn't exist!");
      }

      var template = await _templateRep.GetByIdAsync(id);

      template.Name = model.Name;
      template.Description = model.Description;
      template.Content = model.Content;

      template = _templateRep.Update(template);
      await _templateRep.Save();
      return Ok(template);
    }
  }
}
