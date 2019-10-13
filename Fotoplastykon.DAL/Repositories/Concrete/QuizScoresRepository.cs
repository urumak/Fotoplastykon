using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class QuizScoresRepository : Repository<QuizAnswer>, IQuizAnswersRepository
    {
        public QuizScoresRepository(DatabaseContext context)
            : base(context)
        {
        }

        private DatabaseContext DatabaseContext => Context as DatabaseContext;
    }
}
