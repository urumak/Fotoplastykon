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
    public class PeopleInRolesRepository : Repository<PersonInRole>, IPeopleInRolesRepository
    {
        public PeopleInRolesRepository(DatabaseContext context)
            : base(context)
        {
        }

        private DatabaseContext DatabaseContext => Context as DatabaseContext;

        public async Task<IPaginationResult<PersonInRole>> GetFilmCast(IPager pager, long filmId)
        {
            return await DatabaseContext.PeopleInRoles
                .Include(r => r.Person)
                .Where(r => r.FilmId == filmId && r.Role == Enums.RoleType.Actor)
                .OrderBy(p => p.Person.FirstName)
                .GetPaginationResult(pager);
        }

        public async Task<IPaginationResult<PersonInRole>> GetFilmMakers(IPager pager, long filmId)
        {
            return await DatabaseContext.PeopleInRoles
                .Include(r => r.Person)
                .Where(r => r.FilmId == filmId && r.Role != Enums.RoleType.Actor)
                .OrderBy(p => p.Person.FirstName)
                .GetPaginationResult(pager);
        }
    }
}
