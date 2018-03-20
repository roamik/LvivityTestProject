using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Site.API.Helpers
{
  public interface IUserRoleSeed
  {
    Task Initialize(RoleManager<IdentityRole<Guid>> roleManager);
  }
}
