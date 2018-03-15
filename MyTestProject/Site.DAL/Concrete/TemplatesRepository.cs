using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Site.DAL.Abstract;
using Site.Models.Entities;

namespace Site.DAL.Concrete
{
  public class TemplatesRepository : ITemplatesRepository
  {
    private readonly DatabaseContext _context;

    public TemplatesRepository(DatabaseContext context)
    {
      _context = context;
    }


    public async Task<List<Template>> GetPagedAsync(string id, int page, int count)
    {
      var templateList = await _context.Templates.Where(t => t.UserId == id)
        .Skip(page * count)
        .Take(count)
        .ToListAsync();
      return templateList;
    }

    public virtual async Task<Template> AddAsync(Template template)
    {
      return (await _context.Templates.AddAsync(template)).Entity;
    }



    public async Task<int> Save()
    {
      return await _context.SaveChangesAsync();
    }

    public async Task<Template> GetByIdAsync(object id)
    {
      return await _context.Templates.FindAsync(id);
    }

    public void Delete(Template entity)
    {
      if (_context.Entry(entity).State == EntityState.Detached)
      {
        _context.Templates.Attach(entity);
      }
      _context.Templates.Remove(entity);
    }

    public async Task<bool> ExistAsync(string key)
    {
      return await _context.Templates.AnyAsync(o => o.Id == key);
    }

    public async Task<Template> FirstAsync(string id)
    {
      return await _context.Templates.FirstOrDefaultAsync(o => o.Id == id);
    }

    public virtual Template Update(Template entity)
    {
      if (_context.Entry(entity).State == EntityState.Detached)
      {
        _context.Templates.Attach(entity);
      }
       _context.Entry(entity).State = EntityState.Modified;
      return entity;
    }

    public async Task<int> CountAsync(string id)
    {
      return await _context.Templates.Include(u => u.User).Where(u => u.UserId == id).CountAsync();
    }
  }
}
