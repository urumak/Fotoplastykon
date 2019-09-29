using Fotoplastykon.DAL.Entities.Abstract;
using Fotoplastykon.DAL.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Entities.Concrete
{
    public class FilmPerson : IEntity, IPage
    {
        public FilmPerson()
        {
            Roles = new HashSet<PersonInRole>();
            PageCreations = new HashSet<PersonPageCreation>();
        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public Profession Profession { get; set; }
        public string PagePublicId { get; set; }

        public ICollection<PersonInRole> Roles { get; set; }
        public ICollection<PersonPageCreation> PageCreations { get; set; }
    }


    internal class FilmPersonMappings : IEntityTypeConfiguration<FilmPerson>
    {
        public void Configure(EntityTypeBuilder<FilmPerson> builder)
        {
            builder.ToTable("film_people");
        }
    }
}
