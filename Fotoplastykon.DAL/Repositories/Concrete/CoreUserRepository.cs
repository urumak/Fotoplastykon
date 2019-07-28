using Fotoplastykon.DAL.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fotoplastykon.DAL.Repositories
{
    public class CoreUserRepository : Repository<CoreUser>, ICoreUserRepository
    {
        public CoreUserRepository(DatabaseContext context)
            : base(context)
        {
        }

        private DatabaseContext _context { get { return Context as DatabaseContext; } }

        public IEnumerable<CoreUser> GetNewUsers(int count)
        {
            return _context.CoreUsers.OrderBy(u => u.Id).Take(count).ToList();
        }
    }
}
