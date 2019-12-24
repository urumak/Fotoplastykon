using Fotoplastykon.DAL.Entities.Abstract;
using Fotoplastykon.DAL.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Entities.Concrete
{
    public class InvitationNotification : IEntity, IAuditable
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long FriendId { get; set; }
        public NotificationType Type { get; set; }
        public bool? Accepted { get; set; }
        public DateTime DateCreated { get; set; }

        public User User { get; set; }
        public User Friend { get; set; }
    }
    internal class InvitationNotificationMappings : IEntityTypeConfiguration<InvitationNotification>
    {
        public void Configure(EntityTypeBuilder<InvitationNotification> builder)
        {
            builder.HasOne(p => p.User).WithMany(p => p.InvitationsNotificationsSent).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.Friend).WithMany(p => p.InvitationsNotifications).HasForeignKey(p => p.FriendId).OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("invitation_notifications");
        }
    }
}
