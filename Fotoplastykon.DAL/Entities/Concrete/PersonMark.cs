using Fotoplastykon.DAL.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Entities.Concrete
{
    public class PersonMark : IEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long PersonId { get; set; }
        public decimal? Mark { get; set; }

        public FilmPerson Person { get; set; }
        public User User { get; set; }
    }
    internal class PersonMarkMappings : IEntityTypeConfiguration<PersonMark>
    {
        public void Configure(EntityTypeBuilder<PersonMark> builder)
        {
            builder.HasOne(p => p.User).WithMany(p => p.RatedPeople).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.Person).WithMany(p => p.Marks).HasForeignKey(p => p.PersonId).OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("person_marks");
        }
    }
}
