using Fotoplastykon.DAL.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Entities.Concrete
{
    public class NotificationsReading : IEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public DateTime? LastReadingDate { get; set; }

        public User User { get; set; }
    }

    internal class NotificationsReadingMappings : IEntityTypeConfiguration<NotificationsReading>
    {
        public void Configure(EntityTypeBuilder<NotificationsReading> builder)
        {
            builder.HasOne(p => p.User).WithOne(p => p.NotificationsReading).HasForeignKey<NotificationsReading>(p => p.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("notifications_readings");
        }
    }
}
