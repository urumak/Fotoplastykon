using Fotoplastykon.DAL.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Entities.Concrete
{
    public class QuizQuestion : IEntity
    {
        public QuizQuestion()
        {
            Answers = new HashSet<QuizAnswer>();
        }

        public long Id { get; set; }
        public long QuizId { get; set; }
        public string QuestionText { get; set; }
        public bool IsMultichoice { get; set; }

        public virtual Quiz Quiz { get; set; }
        public virtual ICollection<QuizAnswer> Answers { get; set; }
    }

    internal class QuizQuestionMappings : IEntityTypeConfiguration<QuizQuestion>
    {
        public void Configure(EntityTypeBuilder<QuizQuestion> builder)
        {
            builder.HasOne(p => p.Quiz).WithMany(p => p.Questions).HasForeignKey(p => p.QuizId).OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("quiz_questions");
        }
    }
}
