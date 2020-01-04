using Fotoplastykon.DAL.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Entities.Concrete
{
    public class ForumThreadComment : IEntity, IAuditable
    {
        public long Id { get; set; }
        public long CreatedById { get; set; }
        public DateTime DateCreated { get; set; }
        public long ThreadId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public long? ParentId { get; set; }

        public User CreatedBy { get; set; }
        public ForumThread Thread { get; set; }
        public ForumThreadComment Parent { get; set; }

        public ICollection<ForumThreadComment> Replies { get; set; }
    }
    internal class ForumThreadCommentMappings : IEntityTypeConfiguration<ForumThreadComment>
    {
        public void Configure(EntityTypeBuilder<ForumThreadComment> builder)
        {
            builder.HasOne(p => p.CreatedBy).WithMany(p => p.ForumThreadComments).HasForeignKey(p => p.CreatedById).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.Thread).WithMany(p => p.Comments).HasForeignKey(p => p.ThreadId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.Parent).WithMany(p => p.Replies).HasForeignKey(p => p.ParentId).OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("forum_thread_comments");
        }
    }
}
