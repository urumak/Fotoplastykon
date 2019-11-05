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
    public class FilmWatchingsRepository : Repository<FilmWatching>, IFilmWatchingsRepository
    {
        public FilmWatchingsRepository(DatabaseContext context)
            : base(context)
        {

        }

        private DatabaseContext DatabaseContext => Context as DatabaseContext;

        public async Task<FilmWatching> Get(long userId, long filmId)
        {
            return await DatabaseContext.FilmWatchings.FirstOrDefaultAsync(f => f.FilmId == filmId && f.UserId == userId);
        }

        public async Task<decimal?> GetRating(long filmId)
        {
            return await DatabaseContext.FilmWatchings.Where(f => f.FilmId == filmId).Select(f => f.Mark).AverageAsync();
        }
    }
}
