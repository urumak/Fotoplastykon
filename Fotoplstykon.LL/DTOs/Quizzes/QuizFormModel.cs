using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.DTOs.Quizzes
{
    public class QuizFormModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }

        public List<QuestionFormModel> Questions { get; set; }
    }
}
