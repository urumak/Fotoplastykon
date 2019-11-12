using Fotoplastykon.DAL.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface IForumThreadsRepository : IRepository<ForumThread>
    {
        Task<ForumThread> GetWithCreator(long id);
        Task<IEnumerable<ForumThread>> GetTheMostPopularForFilm(long filmId, int limit = 5);
        Task<IEnumerable<ForumThread>> GetTheMostPopularForFilmPerson(long personId, int limit = 5);
    }
}
