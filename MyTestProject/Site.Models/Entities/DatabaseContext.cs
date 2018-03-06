using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Site.Models.Entities
{
  public class DatabaseContext : IdentityDbContext<User>
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



    //public UserManager<User> UserManager { get; }

    //public SignInManager<User> SignInManager { get; }

    public DbSet<Template> Templates { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<User>()    // User 1 => N Templates
              .HasMany(u => u.Templates)
              .WithOne(uc => uc.User)
              .HasForeignKey(uc => uc.UserId);
    }

  }
}
