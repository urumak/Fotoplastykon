using Fotoplastykon.DAL.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Entities.Concrete
{
    public class Quiz : IEntity
    {
        public Quiz()
        {
            Questions = new HashSet<QuizQuestion>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<QuizQuestion> Questions { get; set; }
        public ICollection<QuizScore> Scores { get; set; }
    }
    internal class QuizMappings : IEntityTypeConfiguration<Quiz>
    {
        public void Configure(EntityTypeBuilder<Quiz> builder)
        {
            builder.ToTable("quizzes");
        }
    }
}
