using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class PersonPageCreationsRepository : Repository<PersonPageCreation>, IPersonPageCreationsRepository
    {
        public PersonPageCreationsRepository(DatabaseContext context)
            : base(context)
        {
        }

        private DatabaseContext DatabaseContext => Context as DatabaseContext;

        public IEnumerable<string> GetPagesIdsForUser(long userId)
        {
            return DatabaseContext.PersonPageCreations
                .Where(c => c.UserId == userId)
                .Select(c => c.Person.PagePublicId)
                .ToList();
        }
    }
}
