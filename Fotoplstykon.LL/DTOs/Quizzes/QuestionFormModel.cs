using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.DTOs.Quizzes
{
    public class QuestionFormModel
    {
        public long Id { get; set; }
        public string QuestionText { get; set; }
        public bool IsMultichoice { get; set; }

        public virtual List<AnswerFormModel> Answers { get; set; }
    }
}
