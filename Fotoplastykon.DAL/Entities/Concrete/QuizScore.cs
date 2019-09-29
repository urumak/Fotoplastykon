using Fotoplastykon.DAL.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Entities.Concrete
{
    public class QuizScore : IEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long QuizId { get; set; }
        public int Score { get; set; }

        public Quiz Quiz { get; set; }
        public User User { get; set; }
    }

    internal class QuizScoreMappings : IEntityTypeConfiguration<QuizScore>
    {
        public void Configure(EntityTypeBuilder<QuizScore> builder)
        {
            builder.HasOne(p => p.Quiz).WithMany(p => p.Scores).HasForeignKey(p => p.QuizId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.User).WithMany(p => p.Scores).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("quiz_scores");
        }
    }
}
