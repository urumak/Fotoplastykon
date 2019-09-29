using Fotoplastykon.DAL.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface IPersonPageCreationRepository : IRepository<PersonPageCreation>
    {
        IEnumerable<string> GetPagesIdsForUser(long userId);
    }
}
