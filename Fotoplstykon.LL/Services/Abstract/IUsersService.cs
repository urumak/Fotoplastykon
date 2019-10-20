using System.Collections.Generic;
using System.Threading.Tasks;
using Fotoplastykon.BLL.Models.Users;
using Fotoplastykon.DAL.Entities.Concrete;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public interface IUsersService
    {
        Task<User> Get(long id);
        Task<bool> Add(AddUserModel user);
        Task<UserLoginModel> GetForLoginByUserName(string userName);
        Task<List<User>> Search(string searchString);
        Task<bool> CheckIfExists(long id);
    }
}
