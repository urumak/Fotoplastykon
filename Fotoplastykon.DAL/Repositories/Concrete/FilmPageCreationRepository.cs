using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class FilmPageCreationRepository : Repository<FilmPageCreation>, IFilmPageCreationRepository
    {
        public FilmPageCreationRepository(DatabaseContext context)
            : base(context)
        {
        }

        private DatabaseContext DatabaseContext => Context as DatabaseContext;

        public IEnumerable<string> GetPagesIdsForUser(long userId)
        {
            return DatabaseContext.FilmPageCreations
                .Where(c => c.UserId == userId)
                .Select(c => c.Film.PagePublicId)
                .ToList();
        }
    }
}
