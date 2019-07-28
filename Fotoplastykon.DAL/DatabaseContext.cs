using Fotoplastykon.DAL.Entities;
using Fotoplastykon.DAL.Entities.Core;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fotoplastykon.DAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public virtual DbSet<CoreUser> CoreUsers { get; protected set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var cs = "Server=localhost;Port=3306;Database=fotoplastykon;User=root;Password=root;Charset=utf8mb4;SslMode=None;Connect Timeout=5;AllowPublicKeyRetrieval=True;";

            var builder = new MySqlConnectionStringBuilder(cs)
            {
                TreatTinyAsBoolean = true,
                OldGuids = true
            };

            optionsBuilder.UseMySql(builder.ToString());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
        }
    }
}
