using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MyTestProject.Models;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MyTestProject.Helpers;
using Microsoft.AspNetCore.Identity;

namespace MyTestProject
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
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

      services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<DatabaseContext>();

      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                      options.RequireHttpsMetadata = false;
                      options.TokenValidationParameters = new TokenValidationParameters
                      {
                        // describes if the publisher will be validated
                        ValidateIssuer = true,
                        // string wich represents the publisher
                        ValidIssuer = AuthOptions.ISSUER,

                        ValidateAudience = true,
                        // token consumer setup
                        ValidAudience = AuthOptions.AUDIENCE,
                        // if the life time will be validated
                        ValidateLifetime = true,

                        // security key
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        // security key validation
                        ValidateIssuerSigningKey = true,
                      };
                    });
      services.AddMvc();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseCors("CorsPolicy");

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
    }
  }
}
