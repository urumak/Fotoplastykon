using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Models.Quizes
{
    public class ResultModel
    {
        public int Points { get; set; }
        public QuizResultModel Quiz { get; set; }
    }
}
