using System.Collections.Generic;
using System.Threading.Tasks;
using Fotoplastykon.DAL.Entities.Concrete;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface IUsersRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetNewUsers(int count);
        Task<User> GetByUserName(string name);
        Task<User> GetByUserNameWithPermissions(string name);
    }
}
