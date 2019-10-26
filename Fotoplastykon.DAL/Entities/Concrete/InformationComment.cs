using Fotoplastykon.DAL.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Entities.Concrete
{
    public class InformationComment : IEntity, IRecoverable, IAuditable
    {
        public InformationComment ()
        {
            Replies = new HashSet<InformationComment>();
        }

        public long Id { get; set; }
        public long InformationId { get; set; }
        public long CreatedById { get; set; }
        public DateTime DateCreated { get; set; }
        public long? ParentId { get; set; }
        public string Content { get; set; }
        public DateTime? DateDeleted { get; set; }

        public InformationComment Parent { get; set; }
        public User CreatedBy { get; set; }
        public ICollection<InformationComment> Replies { get; set; }
    }

    internal class InformationCommentMappings : IEntityTypeConfiguration<InformationComment>
    {
        public void Configure(EntityTypeBuilder<InformationComment> builder)
        {
            builder.HasOne(p => p.Parent).WithMany(p => p.Replies).HasForeignKey(p => p.ParentId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.CreatedBy).WithMany(p => p.InformationComments).HasForeignKey(p => p.CreatedById).OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("information_comments");
        }
    }
}
