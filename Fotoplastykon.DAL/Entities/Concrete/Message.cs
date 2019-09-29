using Fotoplastykon.DAL.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Entities.Concrete
{
    public class Message : IEntity
    {
        public long Id { get; set; }
        public long SenderId { get; set; }
        public long ReceiverId { get; set; }
        public string MessageText { get; set; }

        public User Sender { get; set; }
        public User Receiver { get; set; }
    }

    internal class MessageMappings : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasOne(p => p.Sender).WithMany(p => p.MessagesSent).HasForeignKey(p => p.SenderId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.Receiver).WithMany(p => p.MessagesReceived).HasForeignKey(p => p.ReceiverId).OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("messages");
        }
    }
}
