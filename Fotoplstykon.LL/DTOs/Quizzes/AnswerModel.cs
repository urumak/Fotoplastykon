using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.DTOs.Quizzes
{
    public class AnswerModel
    {
        public long Id { get; set; }
        public string AnswerText { get; set; }
        public bool IsSelected { get; set; }
    }
}
