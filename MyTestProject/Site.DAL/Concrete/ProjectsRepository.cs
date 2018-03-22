using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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


    public async Task<List<Project>> GetPagedAsync(Guid id, int page, int count)
    {
      var projectsList = await _context.Projects.Where(t => t.OwnerId == id)
        .Skip(page * count)
        .Take(count)
        .ToListAsync();
      return projectsList;
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

    public async Task<bool> ExistAsync(Guid key)
    {
      return await _context.Projects.AnyAsync(o => o.Id == key);
    }

    public virtual Project Update(Project project)
    {
      var result = _context.Update(project).Entity;

      _context.SaveChanges();

      return result;
    }

    public async Task<int> CountAsync(Guid id)
    {
      return await _context.Projects.Include(u => u.Id).Where(u => u.OwnerId == id).CountAsync();
    }

    public Task<Project> FirstOrDefaultAsync(Expression<Func<Project, bool>> predicate)
    {
      return _context.Projects.Include(l => l.LinkedUsers).FirstOrDefaultAsync(predicate);
    }

    //public async Task<int> CountAsync(Guid id)
    //{
    //  return await _context.Projects.Include(u => u.Owner).Where(u => u.OwnerId == id).CountAsync();
    //}

    //public Task<Project> FirstOrDefaultAsync(Expression<Func<Project, bool>> predicate)
    //{
    //  return _context.Projects.Include(u => u.Owner).Include(l=>l.LinkedUsers).FirstOrDefaultAsync(predicate);
    //}
  }
}
