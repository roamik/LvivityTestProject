using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTestProject.Models;

namespace MyTestProject.Controllers
{
    [Route("api/[controller]")]
    public class TemplatesController : Controller
    {
        private readonly DatabaseContext _context;

        public TemplatesController(DatabaseContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        [Route("templates")]
        public IActionResult GetLogin()
        {
            return Ok($"Login: {User.Identity.Name}");
        }

        [Authorize(Roles = "Admin, user")]
        [HttpGet]
        [Route("getrole")]
        public IActionResult GetRole()
        {
            return Ok("Role: Administrator");
        }
    }
}
