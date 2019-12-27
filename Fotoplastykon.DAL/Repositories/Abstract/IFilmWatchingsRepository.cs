using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.Tools.Pager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface IFilmWatchingsRepository : IRepository<FilmWatching>
    {
        Task<FilmWatching> Get(long userId, long filmId);
        Task<decimal?> GetRating(long filmId);
        Task<IPaginationResult<FilmWatching>> GetPaginationResultForUser(IPager pager, long userId);
    }
}
