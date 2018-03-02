using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Site.API.Models
{
  public class User : IdentityUser
  {

    public string Name { get; set; }

    //nav
    public List<Template> Templates { get; set; }
  }
}
