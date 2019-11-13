using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class QuizScoresRepository : Repository<QuizScore>, IQuizScoresRepository
    {
        public QuizScoresRepository(DatabaseContext context)
            : base(context)
        {
        }

        private DatabaseContext DatabaseContext => Context as DatabaseContext;
        public async Task<QuizScore> Get(long userId, long quizId)
        {
            return await DatabaseContext.QuizScores.FirstOrDefaultAsync(s => s.UserId == userId && s.QuizId == quizId);
        }
    }
}
