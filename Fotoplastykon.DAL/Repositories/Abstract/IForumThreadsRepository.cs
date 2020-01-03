using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.Tools.Pager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface IForumThreadsRepository : IRepository<ForumThread>
    {
        Task<ForumThread> GetWithCreator(long id);
        Task<IPaginationResult<ForumThread>> GetTheMostPopularForFilm(IPager pager, long filmId);
        Task<IPaginationResult<ForumThread>> GetTheMostPopularForFilmPerson(IPager pager, long personId);
    }
}
