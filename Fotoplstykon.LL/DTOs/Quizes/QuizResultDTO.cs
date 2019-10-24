using System.Collections.Generic;

namespace Fotoplastykon.BLL.DTOs.Quizes
{
    public class QuizResultDTO
    {
        public string Name { get; set; }
        public ICollection<QuestionResultDTO> Questions { get; set; }
    }
}