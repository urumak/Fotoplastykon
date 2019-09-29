using Fotoplastykon.DAL.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Entities.Concrete
{
    public class PersonPageCreation : IEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long PersonId { get; set; }

        public User User { get; set; }
        public FilmPerson Person { get; set; }
    }

    internal class PersonPageCreationMappings : IEntityTypeConfiguration<PersonPageCreation>
    {
        public void Configure(EntityTypeBuilder<PersonPageCreation> builder)
        {
            builder.HasOne(p => p.User).WithMany(p => p.PersonPageCreations).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.Person).WithMany(p => p.PageCreations).HasForeignKey(p => p.PersonId).OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("person_page_creations");
        }
    }
}
