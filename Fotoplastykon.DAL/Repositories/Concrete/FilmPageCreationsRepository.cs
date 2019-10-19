using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class FilmPageCreationsRepository : Repository<FilmPageCreation>, IFilmPageCreationsRepository
    {
        public FilmPageCreationsRepository(DatabaseContext context)
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
