using Microsoft.AspNetCore.Identity;
using Site.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTestProject.Models
{
  public class User : IdentityUser
  {

    public string Name { get; set; }

    //nav
    public List<Template> Templates { get; set; }
  }
}
