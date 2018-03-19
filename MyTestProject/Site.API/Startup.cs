using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Site.API.Configuration;
using System;
using Site.API.Helpers;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Authorization;
using Site.DAL.Abstract;
using Site.DAL.Concrete;
using Site.Models.Entities;
using Site.Models.Options;
using AutoMapper;

namespace Site.API
{
  public class Startup
  {
    private const string TOKEN = "token";

    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.Configure<JWTOptions>(Configuration.GetSection("Tokens"));

      services.AddDbContext<DatabaseContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DatabaseContext")));

      services.AddCors(options =>
      {
        options.AddPolicy("CorsPolicy",
            builder => builder

            .WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
      });

      services.AddScoped<IUserRoleSeed, UserRoleSeed>();

      services.AddScoped<ITemplatesRepository, TemplatesRepository>();

      services.AddScoped<IUsersRepository, UsersRepository>();

      services.AddScoped<IProjectsRepository, ProjectsRepository>();

      services.AddScoped<IdentityDbContext<User>, DatabaseContext>();

      services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<DatabaseContext>()
               .AddDefaultTokenProviders()
               .AddRoleValidator<RoleValidator<IdentityRole>>()
               .AddRoleManager<RoleManager<IdentityRole>>()
               .AddSignInManager<SignInManager<User>>();

      services.Configure<IdentityOptions>(options =>
      {
        // Password settings
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        //options.Password.RequireLowercase = false;
        //options.Password.RequiredUniqueChars = 6;

        // Lockout settings
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
        options.Lockout.MaxFailedAccessAttempts = 10;
        options.Lockout.AllowedForNewUsers = true;

        // User settings
        options.User.RequireUniqueEmail = true;
      });

      //JWT
      services.AddAuthentication(o =>
      {
        o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        o.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(options =>
      {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,

          ValidIssuer = Configuration["Tokens:Issuer"],
          ValidAudience = Configuration["Tokens:Issuer"],
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
        };
      });

      var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

      services.AddAuthorization(options =>
      {
        options.AddPolicy("DefaultPolicy", policy);
        options.DefaultPolicy = policy;
      });

      services.AddAutoMapper();

      services.AddMvc(config =>
      {
        config.Filters.Add(new AuthorizeFilter(policy));
      });

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, IUserRoleSeed roleSeed)
    {
      var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
      app.UseCors("CorsPolicy");
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseSwagger();

      // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
      });

      app.UseDefaultFiles();
      app.UseStaticFiles();

      app.UseAuthentication();

      app.UseMvc();

      using (var scope = scopeFactory.CreateScope())
      {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        ((UserRoleSeed)roleSeed).Initialize(roleManager).Wait();
      }
    }
  }
}
