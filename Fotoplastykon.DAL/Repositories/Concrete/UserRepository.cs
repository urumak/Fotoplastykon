using System.Collections.Generic;
using System.Linq;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext context)
            : base(context)
        {
        }

        private DatabaseContext DatabaseContext => Context as DatabaseContext;

        public IEnumerable<User> GetNewUsers(int count)
        {
            return DatabaseContext.Users.OrderBy(u => u.Id).Take(count).ToList();
        }

        public User GetByUserName(string name)
        {
            return DatabaseContext.Users.FirstOrDefault(u => u.UserName == name);
        }

        public User GetByRefreshToken(string refreshToken)
        {
            return DatabaseContext.Users.FirstOrDefault(u => u.RefreshToken == refreshToken);
        }

        public User GetByUserNameWithPermissions(string name)
        {
            return DatabaseContext.Users.Include(u => u.PageCreations).FirstOrDefault(u => u.UserName == name);
        }
    }
}
