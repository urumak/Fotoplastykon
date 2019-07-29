using System.Collections.Generic;
using System.Linq;
using Fotoplastykon.DAL.Entities.Concrete.Core;
using Fotoplastykon.DAL.Repositories.Abstract;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class CoreUserRepository : Repository<CoreUser>, ICoreUserRepository
    {
        // ReSharper disable once SuggestBaseTypeForParameter
        public CoreUserRepository(DatabaseContext context)
            : base(context)
        {
        }

        private DatabaseContext DatabaseContext => Context as DatabaseContext;

        public IEnumerable<CoreUser> GetNewUsers(int count)
        {
            return DatabaseContext.CoreUsers.OrderBy(u => u.Id).Take(count).ToList();
        }
    }
}
