using Fotoplastykon.DAL.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface IForumThreadsRepository : IRepository<ForumThread>
    {
        Task<ForumThread> GetWithCommentsAndCreator(long id);
    }
}
