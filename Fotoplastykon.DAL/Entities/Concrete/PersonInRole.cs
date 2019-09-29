using Fotoplastykon.DAL.Entities.Abstract;
using Fotoplastykon.DAL.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Entities.Concrete
{
    public class PersonInRole : IEntity
    {
        public long Id { get; set; }
        public long PersonId { get; set; }
        public long FilmId { get; set; }
        public RoleType Role { get; set; }
        public string CharacterName { get; set; }

        public FilmPerson Person { get; set; }
        public Film Film { get; set; }
    }

    internal class PeopleInRolesMappings : IEntityTypeConfiguration<PersonInRole>
    {
        public void Configure(EntityTypeBuilder<PersonInRole> builder)
        {
            builder.HasOne(p => p.Person).WithMany(p => p.Roles).HasForeignKey(p => p.PersonId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.Film).WithMany(p => p.PeopleInRoles).HasForeignKey(p => p.FilmId).OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("people_in_roles");
        }
    }
}
