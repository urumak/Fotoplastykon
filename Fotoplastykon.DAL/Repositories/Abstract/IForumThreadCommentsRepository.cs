using Fotoplastykon.DAL.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface IForumThreadCommentsRepository : IRepository<ForumThreadComment>
    {
        Task<List<ForumThreadComment>> GetList(long threadId);
    }
}
