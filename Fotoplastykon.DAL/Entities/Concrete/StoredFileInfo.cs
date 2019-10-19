using Fotoplastykon.DAL.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Entities.Concrete
{
    public class StoredFileInfo : IEntity
    {
        public long Id { get; set; }
        public string PublicId { get; set; }
        public string AbsolutePath { get; set; }
        public string RelativePath { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }
    }

    internal class FileMappings : IEntityTypeConfiguration<StoredFileInfo>
    {
        public void Configure(EntityTypeBuilder<StoredFileInfo> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(500);
            builder.Property(p => p.AbsolutePath).IsRequired().HasMaxLength(1000);
            builder.Property(p => p.RelativePath).HasMaxLength(1000);
            builder.Property(p => p.PublicId).IsRequired().HasMaxLength(80);
            builder.ToTable("files");
        }
    }
}
