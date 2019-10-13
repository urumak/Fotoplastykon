using System.Collections.Generic;
using Fotoplastykon.BLL.Models.Users;
using Fotoplastykon.DAL.Entities.Concrete;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public interface IUsersService
    {
        User Get(long id);
        bool Add(AddUserModel user);
        UserLoginModel GetForLoginByUserName(string userName);
        List<User> Search(string searchString);
        bool CheckIfExists(long id);
    }
}
