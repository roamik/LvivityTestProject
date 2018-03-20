using Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Site.DAL.Abstract
{
  public interface IProjectsRepository
  {
    Task<List<Project>> GetPagedAsync(Guid id, int page, int count);
    Task<Project> AddAsync(Project template);
    Task<int> Save();
    Task<bool> ExistAsync(Guid key);
    Task<Project> GetByIdAsync(object id);
    void Delete(Project entity);
    Task<Project> FirstAsync(Guid id);
    Project Update(Project entity);
    Task<int> CountAsync(Guid id);
    Task<Project> FirstOrDefaultAsync(Expression<Func<Project, bool>> predicate);
  }
}
