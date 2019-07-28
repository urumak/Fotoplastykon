using Fotoplastykon.DAL.Entities.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Repositories
{
    public interface ICoreUserRepository : IRepository<CoreUser>
    {
        IEnumerable<CoreUser> GetNewUsers(int count);
    }
}
