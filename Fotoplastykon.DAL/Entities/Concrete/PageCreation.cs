using Fotoplastykon.DAL.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fotoplastykon.DAL.Entities.Concrete
{
    public class PageCreation : IEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long FilmId { get; set; }

        public User User { get; set; }
        public Film Film { get; set; }
    }

    internal class PageCreationMappings : IEntityTypeConfiguration<PageCreation>
    {
        public void Configure(EntityTypeBuilder<PageCreation> builder)
        {
            builder.HasOne(p => p.User).WithMany(p =>p.PageCreations).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.Film).WithMany(p => p.PageCreations).HasForeignKey(p => p.FilmId).OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("page_creations");
        }
    }
}
