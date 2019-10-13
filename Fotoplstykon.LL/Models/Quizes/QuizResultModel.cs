using System.Collections.Generic;

namespace Fotoplastykon.BLL.Models.Quizes
{
    public class QuizResultModel
    {
        public string Name { get; set; }
        public ICollection<QuestionResultModel> Questions { get; set; }
    }
}