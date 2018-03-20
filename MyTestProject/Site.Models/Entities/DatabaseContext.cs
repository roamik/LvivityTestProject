using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Site.Models.Entities
{
  public class DatabaseContext : IdentityDbContext<User, IdentityRole<Guid>,Guid>
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
          "Server = DESKTOP-F84JFFC; Database = NewDb; Trusted_Connection = True; MultipleActiveResultSets = true"
          /*"Server = ROAMPC; Database = NewDb; Trusted_Connection = True; MultipleActiveResultSets = true"*/);
    }



    //public UserManager<User> UserManager { get; }

    //public SignInManager<User> SignInManager { get; }

    public DbSet<Template> Templates { get; set; }

    public DbSet<Project> Projects { get; set; }

    public DbSet<UserProject> UserProjects { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      //modelBuilder.Entity<User>()    // User 1 => N Templates
      //        .HasMany(u => u.Templates)
      //        .WithOne(uc => uc.User)
      //        .HasForeignKey(uc => uc.UserId);

      //modelBuilder.Entity<User>()    // User 1 => N Projects
      //  .HasMany(u => u.Projects)
      //  .WithOne(uc => uc.User)
      //  .HasForeignKey(uc => uc.UserId);

      //modelBuilder.Entity<UserProject>().HasKey(e => new { e.UserId, e.ProjectId });

      //modelBuilder.Entity<User>()    // User 1 => N InvolvedProjects
      //  .HasMany(u => u.InvolvedProjects)
      //  .WithOne(uc => uc.User)
      //  .HasForeignKey(uc => uc.UserId);

      //modelBuilder.Entity<UserProject>()    // 1 project contains N LinkedUsers (linked users - users involved in project)
      //  .HasOne(up => up.Project)
      //  .WithMany(c => c.LinkedUsers)
      //  .HasForeignKey(up => up.ProjectId);

    }

  }
}
