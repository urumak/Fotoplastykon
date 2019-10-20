using Fotoplastykon.DAL.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface IQuizesRepository : IRepository<Quiz>
    {
        Task<Quiz> GetFullQuiz(long id);
    }
}
