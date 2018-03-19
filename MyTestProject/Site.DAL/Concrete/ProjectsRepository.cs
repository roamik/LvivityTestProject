using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Site.DAL.Abstract;
using Site.Models.Entities;

namespace Site.DAL.Concrete
{
  public class ProjectsRepository : IProjectsRepository
  {
    private readonly DatabaseContext _context;

    public ProjectsRepository(DatabaseContext context)
    {
      _context = context;
    }


    public async Task<List<Project>> GetPagedAsync(string id, int page, int count)
    {
      var templateList = await _context.Projects.Where(t => t.UserId == id)
        .Skip(page * count)
        .Take(count)
        .ToListAsync();
      return templateList;
    }

    public virtual async Task<Project> AddAsync(Project project)
    {
      return (await _context.Projects.AddAsync(project)).Entity;
    }



    public async Task<int> Save()
    {
      return await _context.SaveChangesAsync();
    }

    public async Task<Project> GetByIdAsync(object id)
    {
      return await _context.Projects.FindAsync(id);
    }

    public void Delete(Project entity)
    {
      if (_context.Entry(entity).State == EntityState.Detached)
      {
        _context.Projects.Attach(entity);
      }
      _context.Projects.Remove(entity);
    }

    public async Task<bool> ExistAsync(string key)
    {
      return await _context.Projects.AnyAsync(o => o.Id == key);
    }

    public async Task<Project> FirstAsync(string id)
    {
      return await _context.Projects.FirstOrDefaultAsync(o => o.Id == id);
    }

    public virtual Project Update(Project project)
    {
      if (_context.Entry(project).State == EntityState.Detached)
      {
        _context.Projects.Attach(project);
      }
      _context.Entry(project).State = EntityState.Modified;
      return project;
    }

    public async Task<int> CountAsync(string id)
    {
      return await _context.Projects.Include(u => u.User).Where(u => u.UserId == id).CountAsync();
    }
  }
}
