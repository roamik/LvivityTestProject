using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Site.Models.Entities
{
  public class User : IdentityUser
  {

    public string Name { get; set; }

    //nav
    public List<Template> Templates { get; set; }
  }
}
