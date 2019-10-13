using Fotoplastykon.DAL.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface IQuizesRepository : IRepository<Quiz>
    {
        Quiz GetFullQuiz(long id);
    }
}
