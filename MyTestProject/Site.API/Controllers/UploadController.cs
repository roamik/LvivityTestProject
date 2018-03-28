using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Site.API.Helpers;
using Site.DAL.Abstract;
using Site.Models.DTO;
using Site.Models.Entities;

namespace Site.API.Controllers
{
  [Authorize(Roles = "Admin,Member")]
  [Route("api/[controller]")]
  public class UploadController : Controller
  {
    private readonly IProjectsRepository _projectsRep;
    private readonly IHostingEnvironment _env;

    private readonly IMapper _mapper;

    public UploadController(IProjectsRepository projectsRep, IMapper mapper, IHostingEnvironment env)
    {
      _projectsRep = projectsRep;
      _env = env;

      _mapper = mapper;
    }

    [HttpPost]
    [Route("{id}")]
    public async Task<IActionResult> UploadProjectImage(Guid id, IFormFile file)
    {
      ImageSaver ims = new ImageSaver();

      var project = await _projectsRep.FirstOrDefaultAsync(p => p.Id == id);

      var filePath = ims.SaveImage(file, _env);


      project.ImagePath = filePath;

      project = _projectsRep.Update(project);

      return Ok(_mapper.Map<ProjectDto>(project));
    }
  }
}
