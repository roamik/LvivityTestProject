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
    public class ValuesController : Controller
    {
        private readonly DatabaseContext _context;

        public ValuesController(DatabaseContext context)
        {
            _context = context;
        }

        //ADD A USER TO DB
        //public void Test()
        //{
        //    var user = new User { Login = "user@gmail.com", Password = "12345", Role = "user" };

        //    _context.Add(user);
        //    _context.SaveChanges();
        //}

        [Authorize]
        [HttpGet]
        [Route("getlogin")]
        public IActionResult GetLogin()
        {
            return Ok($"Login: {User.Identity.Name}");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("getrole")]
        public IActionResult GetRole()
        {
            return Ok("Role: Administrator");
        }
    }
}
