using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class UsersRepository : Repository<User>, IUsersRepository
    {
        public UsersRepository(DatabaseContext context)
            : base(context)
        {
        }

        private DatabaseContext DatabaseContext => Context as DatabaseContext;

        public async Task<IEnumerable<User>> GetNewUsers(int count)
        {
            return await DatabaseContext.Users.OrderBy(u => u.Id).Take(count).ToListAsync();
        }

        public async Task<User> GetByUserName(string name)
        {
            return await DatabaseContext.Users.FirstOrDefaultAsync(u => u.UserName == name);
        }

        public async Task<User> GetByUserNameWithPermissions(string name)
        {
            return await DatabaseContext.Users.Include(u => u.FilmPageCreations).FirstOrDefaultAsync(u => u.UserName == name);
        }
    }
}
