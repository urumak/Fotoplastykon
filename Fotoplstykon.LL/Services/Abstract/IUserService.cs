using System.Collections.Generic;
using Fotoplastykon.LL.Models.Users;

namespace Fotoplastykon.LL.Services.Abstract
{
    public interface IUserService
    {
        IEnumerable<AddUserModel> GetAll();
        bool Add(AddUserModel user);
        UserLoginModel GetForLoginByUserName(string userName);
    }
}
