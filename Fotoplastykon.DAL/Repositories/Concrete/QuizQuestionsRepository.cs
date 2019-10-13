using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class QuizQuestionsRepository : Repository<QuizQuestion>, IQuizQuestionsRepository
    {
        public QuizQuestionsRepository(DatabaseContext context)
            : base(context)
        {
        }

        private DatabaseContext DatabaseContext => Context as DatabaseContext;
    }
}
