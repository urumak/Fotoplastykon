using System.Collections.Generic;

namespace Fotoplastykon.BLL.Models.Quizes
{
    public class QuestionResultModel
    {
        public long Id { get; set; }
        public string QuestionText { get; set; }
        public ICollection<AnswerResultModel> Answers { get; set; }
    }
}