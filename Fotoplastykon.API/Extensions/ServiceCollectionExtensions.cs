using Fotoplastykon.DAL.Entities;
using Fotoplastykon.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using NetCore.AutoRegisterDi;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Fotoplastykon.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterAllTypes(this IServiceCollection services, Assembly assembly, string nameEndsWith)
        {
            services.RegisterAssemblyPublicNonGenericClasses(assembly)
                 .Where(c => c.Name.EndsWith(nameEndsWith))
                 .AsPublicImplementedInterfaces();
        }

        public static void AddMySqlDbContext<TContext>(this IServiceCollection services, IConfiguration configuration) where TContext : DbContext
        {
            services.AddDbContext<TContext>(options =>
            {
                var cs = configuration.GetConnectionString("DefaultConnection");

                var builder = new MySqlConnectionStringBuilder(cs)
                {
                    TreatTinyAsBoolean = true,
                    OldGuids = true
                };

                options.UseMySql(builder.ToString());
            });
        }
    }
}
