using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fotoplastykon.API.Areas.Public.Models.Quizes
{
    public class QuizModel
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<QuestionModel> Questions { get; set; }
    }
}
