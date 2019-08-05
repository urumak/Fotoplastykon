using System.Collections.Generic;
using System.Linq;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class UserRepository : Repository<User>, ICoreUserRepository
    {
        // ReSharper disable once SuggestBaseTypeForParameter
        public UserRepository(DatabaseContext context)
            : base(context)
        {
        }

        private DatabaseContext DatabaseContext => Context as DatabaseContext;

        public IEnumerable<User> GetNewUsers(int count)
        {
            return DatabaseContext.Users.OrderBy(u => u.Id).Take(count).ToList();
        }
    }
}
