using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Entities.Core
{
    public class CoreUser : IEntity
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
    }

    internal class CoreUserMappings : IEntityTypeConfiguration<CoreUser>
    {
        public void Configure(EntityTypeBuilder<CoreUser> builder)
        {
            builder.Property(p => p.UserName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.FirstName).HasMaxLength(100);
            builder.Property(p => p.Surname).HasMaxLength(250);
            builder.Property(p => p.Email).IsRequired().HasMaxLength(250);
            builder.Property(p => p.PasswordHash).IsRequired().HasMaxLength(300);
        }
    }
}
