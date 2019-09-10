using System.Collections.Generic;
using Fotoplastykon.BLL.Models.Users;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public interface IUserService
    {
        IEnumerable<AddUserModel> GetAll();
        bool Add(AddUserModel user);
        UserLoginModel GetForLoginByUserName(string userName);
    }
}
