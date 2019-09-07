using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using NetCore.AutoRegisterDi;
using System.Reflection;

namespace Fotoplastykon.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterAllTypes(this IServiceCollection services, Assembly assembly, string nameEndsWith, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        {
            services.RegisterAssemblyPublicNonGenericClasses(assembly)
                 .Where(c => c.Name.EndsWith(nameEndsWith))
                 .AsPublicImplementedInterfaces(serviceLifetime);
        }

        public static void AddMySqlDbContext<TContext>(this IServiceCollection services, string connectionString) where TContext : DbContext
        {
            services.AddDbContext<TContext>(options =>
            {
                var builder = new MySqlConnectionStringBuilder(connectionString)
                {
                    TreatTinyAsBoolean = true,
                    OldGuids = true
                };

                options.UseMySql(builder.ToString());
            });
        }

        public static void SetAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters.NameClaimType = "sub";
                options.MetadataAddress = "https://login.microsoftonline.com/common/.well-known/openid-configuration";
                options.Audience = "https://myapi.audience.com";
            });
        }

        public static void SetAuthorization(this IServiceCollection services)
        {

        }
    }
}
