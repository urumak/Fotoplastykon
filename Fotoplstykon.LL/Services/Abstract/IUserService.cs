using Fotoplstykon.LL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplstykon.LL.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
    }
}
