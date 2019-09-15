﻿using Fotoplastykon.DAL.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Fotoplastykon.DAL.Entities.Concrete
{
    public class Film : IEntity, IPage
    {
        public Film()
        {
            PageCreations = new HashSet<PageCreation>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public int YearOfProduction { get; set; }
        public string PagePublicId { get; set; }

        public ICollection<PageCreation> PageCreations { get; set; }
    }

    internal class FilmMappings : IEntityTypeConfiguration<Film>
    {
        public void Configure(EntityTypeBuilder<Film> builder)
        {
            builder.Property(p => p.Title).IsRequired().HasMaxLength(500);
            builder.Property(p => p.YearOfProduction).IsRequired();
            builder.Property(p => p.PagePublicId).IsRequired().HasMaxLength(80);
            builder.ToTable("films");
        }
    }
}
