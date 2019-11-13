using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.DTOs.Quizzes
{
    public class QuizModel
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<QuestionModel> Questions { get; set; }
    }
}
