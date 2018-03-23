using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Site.DAL.Abstract;
using Site.Models.Entities;

namespace Site.DAL.Concrete
{
  public class UsersRepository : IUsersRepository
  {
    private readonly DatabaseContext _context;
    private readonly UserManager<User> _userManager;

    public UsersRepository(DatabaseContext context, UserManager<User> userManager)
    {
      _userManager = userManager;
      _context = context;
    }


    public async Task<List<User>> GetPagedAsync(int page, int count, Guid myId)
    {
      var usersList = await _context.Users.Where(u=>u.Id != myId)
        .Skip(page * count)
        .Take(count)
        .ToListAsync();
      return usersList;
    }

    public async Task<int> Save()
    {
      return await _context.SaveChangesAsync();
    }

    public async Task<User> GetByIdAsync(object id)
    {
      return await _context.Users.FindAsync(id);
    }

    public void Delete(User entity)
    {
      if (_context.Entry(entity).State == EntityState.Detached)
      {
        _context.Users.Attach(entity);
      }
      _context.Users.Remove(entity);
    }

    public async Task<bool> ExistAsync(Guid key)
    {
      return await _context.Templates.AnyAsync(o => o.Id == key);
    }

    public async Task<User> FirstAsync(Guid id)
    {
      return await _context.Users.FirstOrDefaultAsync(o => o.Id == id);
    }

    public virtual User Update(User entity)
    {
      if (_context.Entry(entity).State == EntityState.Detached)
      {
        _context.Users.Attach(entity);
      }
      _context.Entry(entity).State = EntityState.Modified;
      return entity;
    }

    public Task<List<User>> GetUsersLinkedAsync(string id, int page, int count)
    {
      throw new NotImplementedException();
    }

    public Task<User> LinkToProjectAsync(Template template)
    {
      throw new NotImplementedException();
    }

    public async Task<int> CountAsync()
    {
      return await _context.Users.CountAsync();
    }
  }
}
