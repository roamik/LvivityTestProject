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
  public class UsersController : Controller
  {
    private readonly IUsersRepository _usersRep;
    private readonly IMapper _mapper;

    public UsersController(IUsersRepository usersRep, IMapper mapper)
    {
      _usersRep = usersRep;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] int page, [FromQuery] int count)
    {
      var myId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value);
      var users = await _usersRep.GetPagedAsync(page, count, myId);
      var usersCount = await _usersRep.CountAsync();

      var pageReturnModel = new PageReturnModel<UserDto>
      {
        Items = _mapper.Map<IEnumerable<UserDto>>(users),
        TotalCount = usersCount,
        CurrentPage = page
      };
      return Ok(pageReturnModel);
    }

  }
}
