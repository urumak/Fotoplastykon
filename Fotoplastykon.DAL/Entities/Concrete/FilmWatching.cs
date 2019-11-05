using Fotoplastykon.DAL.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Entities.Concrete
{
    public class FilmWatching : IEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long FilmId { get; set; }
        public decimal? Mark { get; set; }

        public User User { get; set; }
        public Film Film { get; set; }
    }

    internal class FilmWatchingMappings : IEntityTypeConfiguration<FilmWatching>
    {
        public void Configure(EntityTypeBuilder<FilmWatching> builder)
        {
            builder.HasOne(p => p.User).WithMany(p => p.FilmsWatched).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.Film).WithMany(p => p.Watchings).HasForeignKey(p => p.FilmId).OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("film_watchings");
        }
    }
}
