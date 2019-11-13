using Fotoplastykon.DAL.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface IQuizScoresRepository : IRepository<QuizScore>
    {
        Task<QuizScore> Get(long userId, long quizId);
    }
}
