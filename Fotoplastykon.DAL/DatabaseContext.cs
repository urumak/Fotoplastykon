using Fotoplastykon.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fotoplastykon.DAL.Entities.Concrete;

namespace Fotoplastykon.DAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; protected set; }
        public virtual DbSet<Film> Films { get; protected set; }
        public virtual DbSet<PageCreation> PageCreations { get; protected set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("Server=localhost;Port=3306;Database=fotoplastykon;User=root;Password=root;Charset=utf8mb4;SslMode=None;Connect Timeout=5;AllowPublicKeyRetrieval=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
        }
    }
}
