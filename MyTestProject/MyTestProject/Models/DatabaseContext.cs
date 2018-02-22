using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTestProject.Models
{
    public class DatabaseContext: DbContext
    {

        private readonly IOptions<AppConnectionStrings> _options;

        public DatabaseContext()
        {

        }

        public DatabaseContext(IOptions<AppConnectionStrings> options)
        {
            _options = options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_options?.Value?.DatabaseContext ??
                "Server = DESKTOP-F84JFFC; Database = NewDb; Trusted_Connection = True; MultipleActiveResultSets = true");
        }

        public DbSet<User> Users { get; set; }
    }
}
