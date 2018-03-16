using Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Site.DAL.Abstract
{
  public interface IProjectsRepository
  {
    Task<List<Project>> GetPagedAsync(string id, int page, int count);
    Task<Project> AddAsync(Project template);
    Task<int> Save();
    Task<bool> ExistAsync(string key);
    Task<Project> GetByIdAsync(object id);
    void Delete(Project entity);
    Task<Project> FirstAsync(string id);
    Project Update(Project entity);
    Task<int> CountAsync(string id);
  }
}
