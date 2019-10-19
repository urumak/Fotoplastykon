using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class QuizAnswersRepository : Repository<QuizAnswer>, IQuizAnswersRepository
    {
        public QuizAnswersRepository(DatabaseContext context)
            : base(context)
        {
        }

        private DatabaseContext DatabaseContext => Context as DatabaseContext;
    }
}
