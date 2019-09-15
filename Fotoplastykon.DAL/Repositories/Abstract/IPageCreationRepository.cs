using System.Collections.Generic;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface IPageCreationRepository
    {
        IEnumerable<string> GetPagesIdsForUser(long userId);
    }
}
