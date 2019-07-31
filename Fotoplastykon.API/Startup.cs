using System;
using AutoMapper;
using Fotoplastykon.API.Extensions;
using Fotoplastykon.DAL;
using Fotoplastykon.DAL.Entities.Abstract;
using Fotoplastykon.DAL.Repositories.Abstract;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using Fotoplastykon.LL.Services.Abstract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            services.AddMySqlDbContext<DatabaseContext>(Configuration);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.RegisterAllTypes(typeof(IRepository<IEntity>).Assembly, "Repository");
            services.RegisterAllTypes(typeof(IUnitOfWork).Assembly, "UnitOfWork");
            services.RegisterAllTypes(typeof(IUserService).Assembly, "Service");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
