using Fotoplastykon.DAL.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface IFilmPageCreationsRepository : IRepository<FilmPageCreation>
    {
        Task<IEnumerable<string>> GetPagesIdsForUser(long userId);
    }
}
