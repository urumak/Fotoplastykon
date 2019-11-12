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
    public class ForumThreadCommentsRepository : Repository<ForumThreadComment>, IForumThreadCommentsRepository
    {
        public ForumThreadCommentsRepository(DatabaseContext context)
            : base(context)
        {
        }

        private DatabaseContext DatabaseContext => Context as DatabaseContext;

        public async Task<List<ForumThreadComment>> GetList(long threadId)
        {
            return await DatabaseContext.ForumThreadComments
                .Include(c => c.Replies)
                .ThenInclude(c => c.CreatedBy)
                .Include(c => c.CreatedBy)
                .Where(c => c.ParentId == null && c.ThreadId == threadId)
                .OrderBy(c => c.DateCreated)
                .ToListAsync();
        }
    }
}
