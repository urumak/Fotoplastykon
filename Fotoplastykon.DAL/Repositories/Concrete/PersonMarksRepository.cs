using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class PersonMarksRepository : Repository<PersonMark>, IPersonMarksRepository
    {
        public PersonMarksRepository(DatabaseContext context)
            : base(context)
        {
        }

        private DatabaseContext DatabaseContext => Context as DatabaseContext;

        public PersonMark Get(long userId, long personId)
        {
            return DatabaseContext.PeopleMarks.FirstOrDefault(f => f.PersonId == personId && f.UserId == userId);
        }
    }
}
