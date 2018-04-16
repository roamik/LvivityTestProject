using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nethereum.Geth;
using Nethereum.Web3;
using Site.DAL.Abstract;
using Site.Models.Entities;

namespace Site.DAL.Concrete
{
  public class TransactionRepository : ITransactionRepository
  {
    private readonly DatabaseContext _context;
    private Web3Geth _web3;

    public TransactionRepository(DatabaseContext context)
    {
      _web3 = new Web3Geth();
      _context = context;
    }


    public async Task<List<Transaction>> GetPagedAsync(Guid id, int page, int count)
    {
      var transactionList = await _context.Transactions.Where(t=>t.UserId == id)//.Where(t => t.UserId == id && t.Confirmed == true)
        .Skip(page * count)
        .Take(count)
        .ToListAsync();
      return transactionList;
    }

    public virtual async Task<Transaction> AddAsync(Transaction transaction)
    {
      return (await _context.Transactions.AddAsync(transaction)).Entity;
    }



    public async Task<int> Save()
    {
      return await _context.SaveChangesAsync();
    }

    public async Task<Transaction> GetByIdAsync(object id)
    {
      return await _context.Transactions.FindAsync(id);
    }

    public void Delete(Transaction entity)
    {
      if (_context.Entry(entity).State == EntityState.Detached)
      {
        _context.Transactions.Attach(entity);
      }
      _context.Transactions.Remove(entity);
    }

    public async Task<bool> ExistAsync(Guid key)
    {
      return await _context.Transactions.AnyAsync(o => o.Id == key);
    }

    public async Task<Transaction> FirstAsync(Guid id)
    {
      return await _context.Transactions.FirstOrDefaultAsync(o => o.Id == id);
    }

    public virtual Transaction Update(Transaction entity)
    {
      if (_context.Entry(entity).State == EntityState.Detached)
      {
        _context.Transactions.Attach(entity);
      }
       _context.Entry(entity).State = EntityState.Modified;
      return entity;
    }

    public async Task<int> CountAsync(Guid id)
    {
      return await _context.Transactions.CountAsync();
    }

    public async Task<decimal> GetBalance(string address)
    {
      var balance = await _web3.Eth.GetBalance.SendRequestAsync(address);
      var converted = Web3.Convert.FromWei(balance.Value, 18);
      return converted;
    }


    //public async Task<int> CountAsync(Guid id)
    //{
    //  return await _context.Templates.Include(u => u.User).Where(u => u.UserId == id).CountAsync();
    //}
  }
}
