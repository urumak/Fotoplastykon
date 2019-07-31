using System.Collections.Generic;
using Fotoplastykon.LL.Models;

namespace Fotoplastykon.LL.Services.Abstract
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
    }
}
