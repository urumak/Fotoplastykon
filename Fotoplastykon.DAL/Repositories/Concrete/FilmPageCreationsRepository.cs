using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class FilmPageCreationsRepository : Repository<FilmPageCreation>, IFilmPageCreationsRepository
    {
        public FilmPageCreationsRepository(DatabaseContext context)
            : base(context)
        {
        }

        private DatabaseContext DatabaseContext => Context as DatabaseContext;

        public async Task<IEnumerable<string>> GetPagesIdsForUser(long userId)
        {
            return await DatabaseContext.FilmPageCreations
                .Where(c => c.UserId == userId)
                .Select(c => c.Film.PagePublicId)
                .ToListAsync();
        }
    }
}
