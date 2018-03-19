using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Site.Models.Entities
{
  public class User : IdentityUser
  {

    public string Name { get; set; }

    public override string Email { get; set; }

    //nav
    public virtual List<Template> Templates { get; set; }

    public virtual List<Project> Projects { get; set; }

    public virtual List<UserProject> InvolvedProjects { get; set; } = new List<UserProject>(); // projects where user is involved in
  }
}
