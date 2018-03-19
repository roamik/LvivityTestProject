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
  public class ProjectsController : Controller
  {
    private readonly IProjectsRepository _projectsRep;
    private readonly IMapper _mapper;

    public ProjectsController(IProjectsRepository projectsRep, IMapper mapper)
    {
      _projectsRep = projectsRep;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetMyProjects([FromQuery] int page, [FromQuery] int count)
    {
      var userId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value;
      var projects = await _projectsRep.GetPagedAsync(userId, page, count);
      var projectsCount = await _projectsRep.CountAsync(userId);

      var pageReturnModel = new PageReturnModel<ProjectDto>
      {
        Items = _mapper.Map<IEnumerable<ProjectDto>>(projects),
        TotalCount = projectsCount,
        CurrentPage = page
      };
      return Ok(pageReturnModel);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetProjectByIdAsync(string id)
    {
      var project = await _projectsRep.FirstAsync(id);
      if (project == null)
      {
        return BadRequest("Project not found!");
      }

      return Ok(_mapper.Map<ProjectDto>(project));
    }

    [HttpPost]
    public async Task<IActionResult> CreateProjectAsync([FromBody] ProjectDto model)
    {
      var userId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value; // Get user id from token Sid claim

      model.Id = null;
      model.UserId = userId;

      var project = _mapper.Map<Project>(model);
      project = await _projectsRep.AddAsync(project);

      await _projectsRep.Save();
      return Ok(_mapper.Map<ProjectDto>(project));
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromBody] ProjectDto model, string id)
    {
      if (!ModelState.IsValid || model == null)
      {
        return BadRequest(ModelState);
      }

      if (!await _projectsRep.ExistAsync(id))
      {
        return NotFound($"Item {id} doesn't exist!");
      }
      
      var project = _mapper.Map<Project>(model);

      project = _projectsRep.Update(project);
      await _projectsRep.Save();
      return Ok(_mapper.Map<ProjectDto>(project));
    }
  }
}
