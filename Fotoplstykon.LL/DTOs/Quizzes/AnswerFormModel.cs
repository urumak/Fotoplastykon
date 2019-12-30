using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.DTOs.Quizzes
{
    public class AnswerFormModel
    {
        public long Id { get; set; }
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }
    }
}
