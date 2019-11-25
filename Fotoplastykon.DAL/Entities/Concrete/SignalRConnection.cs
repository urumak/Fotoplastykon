using Fotoplastykon.DAL.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Entities.Concrete
{
    public class SignalRConnection : IEntity, IAuditable, IRecoverable
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string ConnectionId { get; set; }
        public string UserAgent { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateDeleted { get; set; }

        public User User { get; set; }
    }

    internal class SignalRConnectionMappings : IEntityTypeConfiguration<SignalRConnection>
    {
        public void Configure(EntityTypeBuilder<SignalRConnection> builder)
        {
            builder.HasOne(p => p.User).WithMany(p => p.SignalRConnections).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("signalr_connections");
        }
    }
}
