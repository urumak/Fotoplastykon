using System.Collections.Generic;

namespace Fotoplastykon.BLL.DTOs.Quizes
{
    public class QuestionResultDTO
    {
        public long Id { get; set; }
        public string QuestionText { get; set; }
        public ICollection<AnswerResultDTO> Answers { get; set; }
    }
}