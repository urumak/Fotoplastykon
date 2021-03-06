﻿using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class InformationCommentsRepository : Repository<InformationComment>, IInformationCommentsRepository
    {
        public InformationCommentsRepository(DatabaseContext context)
            : base(context)
        {
        }

        private DatabaseContext DatabaseContext => Context as DatabaseContext;

        public async Task<List<InformationComment>> GetList(long informationId)
        {
            return await DatabaseContext.InformationComments
                .Include(c => c.Replies)
                .ThenInclude(c => c.CreatedBy)
                .Include(c => c.CreatedBy)
                .Where(c => c.ParentId == null && c.InformationId == informationId)
                .OrderBy(c => c.DateCreated)
                .ToListAsync();
        }
    }
}
