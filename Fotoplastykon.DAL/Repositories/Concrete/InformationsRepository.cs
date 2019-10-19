using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class InformationsRepository : Repository<Information>, IInformationsRepository
    {
        public InformationsRepository(DatabaseContext context)
            : base(context)
        {
        }

        private DatabaseContext DatabaseContext => Context as DatabaseContext;

        public Information GetWithCreator(long id)
        {
            return Context.Set<Information>().Include(i => i.CreatedBy).FirstOrDefault(i => i.Id == id);
        }
    }
}
