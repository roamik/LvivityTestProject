using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Site.DAL.Abstract;
using Site.Models.Entities;

namespace Site.DAL.Concrete
{
  public class UserProjectRepository : IUserProjectRepository
  {
    private readonly DatabaseContext _context;

    public UserProjectRepository(DatabaseContext context)
    {
      _context = context;
    }

    public void DetachUser(UserProject entity)
    {
      _context.UserProjects.Remove(entity);
      _context.SaveChanges();
    }

    public async Task<UserProject> GetByIdAsync(Guid userId, Guid projId)
    {
      var userProj = await _context.UserProjects.FindAsync(userId, projId);
      return userProj;
    }
  }
}
