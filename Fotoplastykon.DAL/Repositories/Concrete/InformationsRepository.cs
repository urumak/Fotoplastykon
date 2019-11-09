using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class InformationsRepository : Repository<Information>, IInformationsRepository
    {
        public InformationsRepository(DatabaseContext context)
            : base(context)
        {
        }

        private DatabaseContext DatabaseContext => Context as DatabaseContext;

        public async Task<Information> GetWithCreator(long id)
        {
            return await DatabaseContext.Informations.Include(i => i.CreatedBy).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Information>> GetForMainPage(int limit = 5)
        {
            return await DatabaseContext.Informations.OrderByDescending(i => i.DateCreated).Take(limit).ToListAsync();
        }
    }
}
