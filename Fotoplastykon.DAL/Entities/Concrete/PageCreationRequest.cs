using Fotoplastykon.DAL.Entities.Abstract;
using Fotoplastykon.DAL.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Entities.Concrete
{
    public class PageCreationRequest : IEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string PagePublicId { get; set; }
        public PageType PageType { get; set; }

        public User User { get; set; }
    }

    internal class PageCreationRequestMappings : IEntityTypeConfiguration<PageCreationRequest>
    {
        public void Configure(EntityTypeBuilder<PageCreationRequest> builder)
        {
            builder.HasOne(p => p.User).WithMany(p => p.CreationRequests).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("page_creation_requests");
        }
    }
}
