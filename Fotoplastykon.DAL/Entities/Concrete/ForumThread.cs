using Fotoplastykon.DAL.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Entities.Concrete
{
    public class ForumThread : IEntity, IAuditable
    {
        public ForumThread ()
        {
            Comments = new HashSet<ForumThreadComment>();
        }

        public long Id { get; set; }
        public long CreatedById { get; set; }
        public DateTime DateCreated { get; set; }
        public long? PersonId { get; set; }
        public long? FilmId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public User CreatedBy { get; set; }
        public FilmPerson Person { get; set; }
        public Film Film { get; set; }

        public ICollection<ForumThreadComment> Comments { get; set; }
    }

    internal class ForumThreadMappings : IEntityTypeConfiguration<ForumThread>
    {
        public void Configure(EntityTypeBuilder<ForumThread> builder)
        {
            builder.HasOne(p => p.CreatedBy).WithMany(p => p.ForumThreads).HasForeignKey(p => p.CreatedById).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.Person).WithMany(p => p.ForumThreads).HasForeignKey(p => p.PersonId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.Film).WithMany(p => p.ForumThreads).HasForeignKey(p => p.FilmId).OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("forum_threads");
        }
    }
}
