using System.Collections.Generic;

namespace Fotoplastykon.BLL.DTOs.Quizzes
{
    public class QuizResultDTO
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public ICollection<QuestionResultDTO> Questions { get; set; }
    }
}