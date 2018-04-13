using Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Site.DAL.Abstract
{
  public interface ITransactionRepository
  {
    Task<List<Transaction>> GetPagedAsync(Guid id, int page, int count);
    Task<Transaction> AddAsync(Transaction template);
    Task<int> Save();
    Task<bool> ExistAsync(Guid key);
    Task<Transaction> GetByIdAsync(object id);
    void Delete(Transaction entity);
    Task<Transaction> FirstAsync(Guid id);
    Transaction Update(Transaction entity);
    Task<int> CountAsync(Guid id);
    Task<decimal> GetBalance(string address);
  }
}
