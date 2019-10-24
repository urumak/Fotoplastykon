using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.DTOs.Quizes
{
    public class ResultDTO
    {
        public int Points { get; set; }
        public QuizResultDTO Quiz { get; set; }
    }
}
