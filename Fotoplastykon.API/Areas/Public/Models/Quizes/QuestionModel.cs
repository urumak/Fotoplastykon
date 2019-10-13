using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fotoplastykon.API.Areas.Public.Models.Quizes
{
    public class QuestionModel
    {

        public long Id { get; set; }
        public string QuestionText { get; set; }
        public bool IsMultichoice { get; set; }

        public virtual ICollection<AnswerModel> Answers { get; set; }
    }
}
