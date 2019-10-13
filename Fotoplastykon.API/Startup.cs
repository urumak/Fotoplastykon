﻿using System;
using AutoMapper;
using Fotoplastykon.API.Extensions;
using Fotoplastykon.DAL;
using Fotoplastykon.DAL.Entities.Abstract;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using Fotoplastykon.BLL.Services.Abstract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Authorization;
using Fotoplastykon.API.AccessHandlers.PageAccess;

namespace Fotoplastykon.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public string FrontendCors { get; } = "frontendCORS";

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddMySqlDbContext<DatabaseContext>(Configuration.GetConnectionString("DefaultConnection"));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.SetAuthentication(Configuration["Tokens:Issuer"], Configuration["Tokens:Key"]);
            services.SetAuthorization();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>().HttpContext?.User ?? new ClaimsPrincipal());
            services.AddTransient<IAuthorizationHandler, PageAccessHandler>();

            services.RegisterAllTypes(typeof(IRepository<IEntity>).Assembly, "Repository");
            services.RegisterAllTypes(typeof(IUnitOfWork).Assembly, "UnitOfWork");
            services.RegisterAllTypes(typeof(IUsersService).Assembly, "Service");

            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            services.AddCors(options =>
            {
                options.AddPolicy(FrontendCors,
                builder =>
                {
                    builder.WithOrigins(Configuration["Cors:Frontend"])
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(FrontendCors);
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
