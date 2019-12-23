using Fotoplastykon.DAL.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Entities.Concrete
{
    public class Conversation : IEntity
    {
        public long Id { get; set; }
        public long FirstUserId { get; set; }
        public long SecondUserId { get; set; }
        public long LastMessageId { get; set; }

        public User FirstUser { get; set; }
        public User SecondUser { get; set; }
        public Message LastMessage { get; set; }
    }

    internal class ConversationMappings : IEntityTypeConfiguration<Conversation>
    {
        public void Configure(EntityTypeBuilder<Conversation> builder)
        {
            builder.HasOne(p => p.FirstUser).WithMany(p => p.ConversationsFirst).HasForeignKey(p => p.FirstUserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.SecondUser).WithMany(p => p.ConversationsSecond).HasForeignKey(p => p.SecondUserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.LastMessage).WithOne(p => p.Conversation).HasForeignKey<Conversation>(p => p.LastMessageId);
            builder.ToTable("conversations");
        }
    }
}
