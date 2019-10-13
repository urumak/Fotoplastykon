using Fotoplastykon.DAL.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface IPersonPageCreationsRepository : IRepository<PersonPageCreation>
    {
        IEnumerable<string> GetPagesIdsForUser(long userId);
    }
}
