using Fotoplastykon.DAL.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Entities.Concrete
{
    public class Friendship : IEntity
    {
        public long Id { get; set; }
        public long InvitingId { get; set; }
        public long InvitedId { get; set; }

        public User Inviting { get; set; }
        public User Invited { get; set; }
    }

    internal class FriendshipMappings : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder.HasOne(p => p.Inviting).WithMany(p => p.InvitedFriends).HasForeignKey(p => p.InvitingId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.Invited).WithMany(p => p.AcceptedFriends).HasForeignKey(p => p.InvitedId).OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("friendships");
        }
    }
}
