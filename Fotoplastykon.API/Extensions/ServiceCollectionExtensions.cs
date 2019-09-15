using Fotoplastykon.API.AccessHandlers.PageAccess;
using Fotoplastykon.BLL.Services.Abstract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using NetCore.AutoRegisterDi;
using System;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

        public static void SetAuthentication(this IServiceCollection services, string issuer, string tokenKey)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = issuer,
                    ValidAudience = issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),

                    RequireExpirationTime = true,
                    ClockSkew = new TimeSpan(0, 5, 0)
                };
            });
        }

        public static void SetAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminAccess", policy => policy.RequireClaim("IsAdmin", "True"));
                options.AddPolicy("CanEditPages", policy =>
                        policy.Requirements.Add(new PageAccessRequirement()));
            });
        }
    }
}
