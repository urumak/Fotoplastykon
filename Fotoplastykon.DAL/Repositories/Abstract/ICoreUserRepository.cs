using System.Collections.Generic;
using Fotoplastykon.DAL.Entities.Concrete.Core;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface ICoreUserRepository : IRepository<CoreUser>
    {
        IEnumerable<CoreUser> GetNewUsers(int count);
    }
}
