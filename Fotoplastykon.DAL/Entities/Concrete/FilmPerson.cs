﻿using Fotoplastykon.DAL.Entities.Abstract;
using Fotoplastykon.DAL.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Entities.Concrete
{
    public class FilmPerson : IEntity
    {
        public FilmPerson()
        {
            Roles = new HashSet<PersonInRole>();
            ForumThreads = new HashSet<ForumThread>();
            Marks = new HashSet<PersonMark>();
        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public Profession Profession { get; set; }
        public string PublicId { get; set; }
        public long? PhotoId { get; set; }

        public StoredFileInfo Photo { get; set; }
        public ICollection<PersonInRole> Roles { get; set; }
        public ICollection<ForumThread> ForumThreads { get; set; }
        public ICollection<PersonMark> Marks { get; set; }
    }


    internal class FilmPersonMappings : IEntityTypeConfiguration<FilmPerson>
    {
        public void Configure(EntityTypeBuilder<FilmPerson> builder)
        {
            builder.HasOne(p => p.Photo).WithMany(p => p.FilmPersonPhotos).HasForeignKey(p => p.PhotoId).OnDelete(DeleteBehavior.SetNull);
            builder.ToTable("film_people");
        }
    }
}
