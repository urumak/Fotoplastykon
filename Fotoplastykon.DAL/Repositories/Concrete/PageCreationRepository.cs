using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class PageCreationRepository : Repository<PageCreation>, IPageCreationRepository
    {
        public PageCreationRepository(DatabaseContext context)
            : base(context)
        {
        }

        private DatabaseContext DatabaseContext => Context as DatabaseContext;

        public IEnumerable<long> GetPagesIdsForUser(long userId)
        {
            return DatabaseContext.PageCreations
                .Where(c => c.UserId == userId)
                .Select(c => c.FilmId)
                .ToList();
        }
    }
}
