using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class QuizScoresRepository : Repository<QuizScore>, IQuizScoresRepository
    {
        public QuizScoresRepository(DatabaseContext context)
            : base(context)
        {
        }

        private DatabaseContext DatabaseContext => Context as DatabaseContext;
    }
}
