using Fotoplastykon.DAL.Entities;
using Fotoplastykon.DAL.Entities.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fotoplastykon.DAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        public DatabaseContext()
        {
        }

        public virtual DbSet<CoreUser> CoreUsers { get; protected set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("Server=localhost;Port=3306;Database=fotoplastykon;User=root;Password=root;Charset=utf8mb4;SslMode=None;Connect Timeout=5;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var types = GetAllTypesForModelCreating();

            foreach (var type in types)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }

        }

        private IEnumerable<Type> GetAllTypesForModelCreating()
        {
            var entityConfigurationType = typeof(IEntityTypeConfiguration<IEntity>);

            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => entityConfigurationType.IsAssignableFrom(t));
        }
    }
}
