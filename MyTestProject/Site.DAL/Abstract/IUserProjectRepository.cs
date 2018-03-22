using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Site.Models.Entities;

namespace Site.DAL.Abstract
{
  public interface IUserProjectRepository
  {
    void DetachUser(UserProject entity);
    Task<UserProject> GetByIdAsync(Guid userid, Guid projId);
  }
}
