using System.Collections.Generic;
using Fotoplastykon.DAL.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fotoplastykon.DAL.Entities.Concrete
{
    public class User : IEntity
    {
        public User()
        {
            PageCreations = new HashSet<PageCreation>();
        }

        public long Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public string RefreshToken { get; set; }

        public ICollection<PageCreation> PageCreations { get; set; }
    }

    internal class UserMappings : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.UserName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.FirstName).HasMaxLength(100);
            builder.Property(p => p.Surname).HasMaxLength(250);
            builder.Property(p => p.Email).IsRequired().HasMaxLength(250);
            builder.Property(p => p.PasswordHash).HasMaxLength(300);
            builder.Property(p => p.IsAdmin).IsRequired().HasDefaultValue(false);
            builder.Property(p => p.RefreshToken).HasMaxLength(300);
            builder.ToTable("users");
        }
    }
}
