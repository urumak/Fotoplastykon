using System.Collections.Generic;
using Fotoplastykon.DAL.Entities.Concrete;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetNewUsers(int count);
        User GetByUserName(string name);
        User GetByRefreshToken(string refreshToken);
        User GetByUserNameWithPermissions(string name);
    }
}
