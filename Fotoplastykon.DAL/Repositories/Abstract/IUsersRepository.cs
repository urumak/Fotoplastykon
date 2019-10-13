using System.Collections.Generic;
using Fotoplastykon.DAL.Entities.Concrete;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface IUsersRepository : IRepository<User>
    {
        IEnumerable<User> GetNewUsers(int count);
        User GetByUserName(string name);
        User GetByUserNameWithPermissions(string name);
    }
}
