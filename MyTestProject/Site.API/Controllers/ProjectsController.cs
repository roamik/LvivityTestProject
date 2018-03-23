using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Remotion.Linq.Clauses;
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
    private readonly IUserProjectRepository _userProjectRep;
    private readonly IUsersRepository _usersRep;
    private readonly IMapper _mapper;

    public ProjectsController(IProjectsRepository projectsRep, IUsersRepository usersRep,IUserProjectRepository userProjectRep, IMapper mapper)
    {
      _projectsRep = projectsRep;
      _userProjectRep = userProjectRep;
      _usersRep = usersRep;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetMyProjects([FromQuery] int page, [FromQuery] int count)
    {
      var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value);
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
    public async Task<IActionResult> GetProjectByIdAsync(Guid id)
    {
      var project = await _projectsRep.FirstOrDefaultAsync(p => p.Id == id);

      if (project == null)
      {
        return BadRequest("Project not found!");
      }

      var mapped = _mapper.Map<ProjectDto>(project);

      foreach (var userProject in mapped.LinkedUsers)
      {
        var user = await _usersRep.FirstAsync(userProject.UserId);
        var mappedUser = _mapper.Map<UserDto>(user);
        userProject.User = mappedUser;
      }

      return Ok(mapped);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProjectAsync([FromBody] ProjectDto model)
    {
      var userId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value; // Get user id from token Sid claim

      //model.Id = null;
      model.OwnerId = Guid.Parse(userId);

      var project = _mapper.Map<Project>(model);
      project = await _projectsRep.AddAsync(project);

      await _projectsRep.Save();
      return Ok(_mapper.Map<ProjectDto>(project));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ProjectDtoInput model)
    {

      if (!ModelState.IsValid || model == null)
      {
        return BadRequest(ModelState);
      }

      if (!await _projectsRep.ExistAsync(model.Id))
      {
        return NotFound($"Item {model.Name} doesn't exist!");
      }

      var project = await _projectsRep.FirstOrDefaultAsync(p => p.Id == model.Id);

      project.Name = model.Name;

      project.Description = model.Description;

      project.Content = model.Content;

      project.Image = model.Image;

      //var project = _mapper.Map<Project>(model);

      foreach (var modelUser in model.LinkedUsers)
      {
        var userList = project.LinkedUsers.Where(c => c.UserId == modelUser.UserId).ToList(); // find users in project.LinkedUsers whos id == models user id

       if (userList.Count == 0 || project.LinkedUsers.Count == 0) //if we found users that are already linked to the project we dont want to duplicate them
        {
          project.LinkedUsers.Add(new UserProject { ProjectId = modelUser.ProjectId, UserId = modelUser.UserId });
        }

      }

      project = _projectsRep.Update(project);

      return Ok(_mapper.Map<ProjectDto>(project));
    }

    [HttpPost]
    [Route("detach")]
    public async Task<IActionResult> DetachUser([FromBody] UserProjectDto model)
    {
      var userid = model.UserId;

      var projId = model.ProjectId;

      var userProject = await _userProjectRep.GetByIdAsync(userid, projId);

      _userProjectRep.DetachUser(userProject);
      return NoContent();
    }
  }
}
