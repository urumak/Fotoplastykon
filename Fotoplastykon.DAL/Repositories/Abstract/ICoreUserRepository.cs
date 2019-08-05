using System.Collections.Generic;
using Fotoplastykon.DAL.Entities.Concrete;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface ICoreUserRepository : IRepository<User>
    {
        IEnumerable<User> GetNewUsers(int count);
    }
}
