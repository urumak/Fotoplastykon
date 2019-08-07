﻿using System.Collections.Generic;
using Fotoplastykon.DAL.Entities.Concrete;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetNewUsers(int count);
        User GetByName(string name);
    }
}