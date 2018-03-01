using Site.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Site.API.DAL.Abstract
{
  public interface ITemplatesRepository
  {
    Task<List<Template>> GetFilteredByUserIdAsync(string id);
    Task<Template> AddAsync(Template template);
    Task<int> Save();
  }
}
