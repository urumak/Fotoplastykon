using Fotoplastykon.DAL.Entities.Concrete;
using System.Collections.Generic;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface IFilmPageCreationsRepository : IRepository<FilmPageCreation>
    {
        IEnumerable<string> GetPagesIdsForUser(long userId);
    }
}
