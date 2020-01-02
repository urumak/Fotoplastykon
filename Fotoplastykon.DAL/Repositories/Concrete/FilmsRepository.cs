using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using Fotoplastykon.Tools.Pager;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class FilmsRepository : Repository<Film>, IFilmsRepository
    {
        public FilmsRepository(DatabaseContext context)
            : base(context)
        {
        }

        private DatabaseContext DatabaseContext => Context as DatabaseContext;

        public async Task<List<Film>> GetForSearch(string search, int limit = 10)
        {
            var films = await DatabaseContext.Films
                .Where(f => f.Title.StartsWith(search)).OrderBy(f => f.Title).OrderBy(f => f.Title).Take(limit).ToListAsync();

            if(films == null || films.Count == 0) films = await DatabaseContext.Films
                    .Where(f => f.Title.Contains(search)).OrderBy(f => f.Title).OrderBy(f => f.Title).Take(limit).ToListAsync();

            return films;
        }

        public async Task<Film> GetForPage(long id)
        {
            return await DatabaseContext.Films
                .Include(u => u.Watchings)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Film> GetWithPeopleInRoles(long id)
        {
            return await DatabaseContext.Films
                .Include(f => f.PeopleInRoles)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
