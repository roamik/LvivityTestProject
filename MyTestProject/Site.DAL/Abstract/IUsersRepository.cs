using Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Site.DAL.Abstract
{
  public interface IUsersRepository
  {
    Task<List<User>> GetPagedAsync(int page, int count);
    Task<List<User>> GetUsersLinkedAsync(string id, int page, int count);
    Task<User> LinkToProjectAsync(Template template);
    Task<int> Save();
    Task<bool> ExistAsync(string key);
    void Delete(User entity);
    Task<int> CountAsync();
  }
}
