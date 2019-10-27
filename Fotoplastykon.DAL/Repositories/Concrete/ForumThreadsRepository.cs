﻿using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using Fotoplastykon.Tools.Pager;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class ForumThreadsRepository : Repository<ForumThread>, IForumThreadsRepository
    {
        public ForumThreadsRepository(DatabaseContext context)
            : base(context)
        {
        }

        private DatabaseContext DatabaseContext => Context as DatabaseContext;

        public async Task<ForumThread> GetWithCommentsAndCreator(long id)
        {
            return await DatabaseContext.ForumThreads.Include(t => t.CreatedBy).Include(t => t.Comments).ThenInclude(c => c.Replies).FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
