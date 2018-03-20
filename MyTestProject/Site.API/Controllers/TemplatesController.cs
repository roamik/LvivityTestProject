using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Site.DAL.Abstract;
using Site.Models.DTO;
using Site.Models.Entities;
using Site.Models.Helpers;

namespace Site.API.Controllers
{

  [Route("api/[controller]")]
  [Authorize(Roles = "Admin,Member")]
  public class TemplatesController : Controller
  {
    private readonly ITemplatesRepository _templateRep;
    private readonly IMapper _mapper;

    public TemplatesController(ITemplatesRepository templateRep, IMapper mapper)
    {
      _templateRep = templateRep;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetMyTemplates([FromQuery] int page, [FromQuery] int count)
    {
      var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value);
      var templates = await _templateRep.GetPagedAsync(userId, page, count);
      var templatesCount = await _templateRep.CountAsync(userId);

      var pageReturnModel = new PageReturnModel<TemplateDto>
      {
        Items = _mapper.Map<IEnumerable<TemplateDto>>(templates),
        TotalCount = templatesCount,
        CurrentPage = page
      };
      return Ok(pageReturnModel);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetTemplateByIdAsync(Guid id)
    {
      var template = await _templateRep.FirstAsync(id);
      if (template == null)
      {
        return BadRequest("Template not found!");
      }

      return Ok(_mapper.Map<TemplateDto>(template));
    }

    [HttpPost]
    public async Task<IActionResult> CreateTemplateAsync([FromBody] TemplateDto model)
    {
      var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value); // Get user id from token Sid claim
      //var template = new Template { Content = model.Content, Description = model.Description, Name = model.Name, UserId = userId };
      model.Id = null;
      model.UserId = userId;
      var template = _mapper.Map<Template>(model);
      template = await _templateRep.AddAsync(template);
      await _templateRep.Save();
      return Ok(_mapper.Map<TemplateDto>(template));
    }

    [HttpDelete]
    [Route("{id}")]
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
    [Route("{id}")]
    public async Task<IActionResult> Update([FromBody] TemplateDto model, Guid id)
    {
      if (!ModelState.IsValid || model == null)
      {
        return BadRequest(ModelState);
      }

      if (!await _templateRep.ExistAsync(id))
      {
        return NotFound($"Item {id} doesn't exist!");
      }

      //var template = await _templateRep.GetByIdAsync(id);
      ////template.Name = model.Name;
      ////template.Description = model.Description;
      ////template.Content = model.Content;
      var template = _mapper.Map<Template>(model);

      template = _templateRep.Update(template);
      await _templateRep.Save();
      return Ok(_mapper.Map<TemplateDto>(template));
    }
  }
}
