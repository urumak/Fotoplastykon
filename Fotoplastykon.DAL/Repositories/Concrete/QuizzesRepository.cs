using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class QuizzesRepository : Repository<Quiz>, IQuizzesRepository
    {
        public QuizzesRepository(DatabaseContext context)
            : base(context)
        {
        }

        private DatabaseContext DatabaseContext => Context as DatabaseContext;

        public async Task<Quiz> GetFullQuiz(long id)
        {
            return await DatabaseContext.Quizes
                .Include(q => q.Questions)
                .ThenInclude(qq => qq.Answers)
                .FirstOrDefaultAsync(q => q.Id == id);
        }
    }
}
