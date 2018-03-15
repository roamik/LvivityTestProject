using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Site.Models.Entities
{
  public class User : IdentityUser
  {

    public string Name { get; set; }

    //nav
    public virtual List<Template> Templates { get; set; }

    public virtual List<Project> Projects { get; set; }

    public virtual List<UserProject> InvolvedProjects { get; set; } // projects where user is involved in
  }
}
