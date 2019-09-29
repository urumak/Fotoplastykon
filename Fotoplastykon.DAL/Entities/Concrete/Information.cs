using Fotoplastykon.DAL.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Entities.Concrete
{
    public class Information : IEntity
    {
        public long Id { get; set; }
        public long CreatedById { get; set; }
        public DateTime DateCreated { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public User CreatedBy { get; set; }
    }

    internal class InformationMappings : IEntityTypeConfiguration<Information>
    {
        public void Configure(EntityTypeBuilder<Information> builder)
        {
            builder.HasOne(p => p.CreatedBy).WithMany(p => p.Informations).HasForeignKey(p => p.CreatedById).OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("informations");
        }
    }
}
