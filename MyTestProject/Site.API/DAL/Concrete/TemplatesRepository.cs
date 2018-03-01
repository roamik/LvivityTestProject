using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTestProject.Models;
using Site.API.DAL.Abstract;
using Site.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Site.API.DAL.Concrete
{
  public class TemplatesRepository : ITemplatesRepository
  {
    private readonly DatabaseContext _context;

    public TemplatesRepository(DatabaseContext context)
    {
      _context = context;
    }


    public async Task<List<Template>> GetFilteredByUserIdAsync(string id)
    {
      var templateList = await _context.Templates.Where(t => t.UserId == id).ToListAsync();
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
  }
}
