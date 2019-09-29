using Fotoplastykon.DAL.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Entities.Concrete
{
    public class QuizAnswer : IEntity
    {
        public long Id { get; set; }
        public long QuestionId { get; set; }
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }

        public QuizQuestion Question { get; set; }
    }

    internal class QuizAnswerMappings : IEntityTypeConfiguration<QuizAnswer>
    {
        public void Configure(EntityTypeBuilder<QuizAnswer> builder)
        {
            builder.HasOne(p => p.Question).WithMany(p => p.Answers).HasForeignKey(p => p.QuestionId).OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("quiz_answers");
        }
    }
}
