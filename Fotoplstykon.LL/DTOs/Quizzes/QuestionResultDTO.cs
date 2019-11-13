using System.Collections.Generic;

namespace Fotoplastykon.BLL.DTOs.Quizzes
{
    public class QuestionResultDTO
    {
        public long Id { get; set; }
        public string QuestionText { get; set; }
        public ICollection<AnswerResultDTO> Answers { get; set; }
    }
}