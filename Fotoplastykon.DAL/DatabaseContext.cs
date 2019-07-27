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

        public virtual DbSet<CoreUser> CoreUsers { get; protected set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entityConfigurationType = typeof(IEntityTypeConfiguration<IEntity>);

            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => entityConfigurationType.IsAssignableFrom(t));

            foreach(var type in types)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
        }
    }
}
