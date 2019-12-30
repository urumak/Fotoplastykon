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
        public string RelativePath { get; set; }
        public string DisplayName { get; set; }
        public string UniqueName { get; set; }
        public string MimeType { get; set; }
        public long Size { get; set; }

        public ICollection<User> UserPhotos { get; set; }
        public ICollection<Film> FilmPhotos { get; set; }
        public ICollection<FilmPerson> FilmPersonPhotos { get; set; }
        public ICollection<Information> InformationPhotos { get; set; }
        public Quiz QuizPhoto { get; set; }
    }

    internal class FileMappings : IEntityTypeConfiguration<StoredFileInfo>
    {
        public void Configure(EntityTypeBuilder<StoredFileInfo> builder)
        {
            builder.Property(p => p.DisplayName).IsRequired().HasMaxLength(500);
            builder.Property(p => p.RelativePath).HasMaxLength(1000);
            builder.Property(p => p.PublicId).IsRequired().HasMaxLength(80);
            builder.Property(p => p.UniqueName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.MimeType).IsRequired().HasMaxLength(500);
            builder.ToTable("files");
        }
    }
}
