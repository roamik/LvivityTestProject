using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Site.DAL.Abstract;
using Site.Models.Entities;

namespace Site.DAL.Concrete
{
  public class GenericRepository<T> : IGenericRepository<T> where T : class
  {
    private readonly DatabaseContext _context;
    private readonly DbSet<T> _dbSet;
    public GenericRepository(DatabaseContext context)
    {
      _context = context;
      _dbSet = context.Set<T>();
    }

    public virtual IQueryable<T> Data => _dbSet;

    
  }
}
