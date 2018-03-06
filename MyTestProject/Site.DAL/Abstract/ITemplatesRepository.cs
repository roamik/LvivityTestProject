using Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Site.DAL.Abstract
{
  public interface ITemplatesRepository
  {
    Task<List<Template>> GetFilteredByUserIdAsync(string id);
    Task<Template> AddAsync(Template template);
    Task<int> Save();
    Task<bool> ExistAsync(Guid key);
    Task<Template> GetByIdAsync(object id);
    void Delete(Template entity);
    Task<Template> FirstAsync(Guid id);
    Template Update(Template entity);
  }
}
