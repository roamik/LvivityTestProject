using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Site.API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Site.API.Helpers
{
  public class UserRoleSeed : IUserRoleSeed
  {
    
    public async Task Initialize(RoleManager<IdentityRole<Guid>> roleManager)
    {
      
      string[] roleNames = { "Admin", "Manager", "Member" };
      IdentityResult roleResult;

      foreach (var roleName in roleNames)
      {
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
          //create the roles and seed them to the database
          roleResult = await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
        }
      }
    }
  }
}
