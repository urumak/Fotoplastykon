using Fotoplastykon.DAL.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Entities.Concrete
{
    public class Invitation : IEntity
    {
        public long Id { get; set; }
        public long InvitingId { get; set; }
        public long InvitedId { get; set; }

        public User Inviting { get; set; }
        public User Invited { get; set; }
    }

    internal class InvitationMappings : IEntityTypeConfiguration<Invitation>
    {
        public void Configure(EntityTypeBuilder<Invitation> builder)
        {
            builder.HasOne(p => p.Inviting).WithMany(p => p.InvitationsSent).HasForeignKey(p => p.InvitingId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.Invited).WithMany(p => p.InvitationsReceived).HasForeignKey(p => p.InvitedId).OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("invitations");
        }
    }
}
