using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class PersonMarksRepository : Repository<PersonMark>, IPersonMarksRepository
    {
        public PersonMarksRepository(DatabaseContext context)
            : base(context)
        {
        }

        private DatabaseContext DatabaseContext => Context as DatabaseContext;

        public async Task<PersonMark> Get(long userId, long personId)
        {
            return await DatabaseContext.PeopleMarks.FirstOrDefaultAsync(f => f.PersonId == personId && f.UserId == userId);
        }

        public async Task<decimal?> GetRating(long personId)
        {
            return await DatabaseContext.PeopleMarks.Where(f => f.PersonId == personId).Select(f => f.Mark).AverageAsync();
        }
    }
}
