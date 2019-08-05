using Fotoplastykon.DAL.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Fotoplastykon.DAL.Entities.Concrete
{
    public class Film : IEntity
    {
        public Film()
        {
            PageCreations = new HashSet<PageCreation>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public int YearOfProduction { get; set; }

        public ICollection<PageCreation> PageCreations { get; set; }
    }

    internal class FilmMappings : IEntityTypeConfiguration<Film>
    {
        public void Configure(EntityTypeBuilder<Film> builder)
        {
            builder.Property(p => p.Title).IsRequired().HasMaxLength(500);
            builder.Property(p => p.YearOfProduction).IsRequired();
            builder.ToTable("films");
        }
    }
}
