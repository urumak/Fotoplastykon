﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class UsersRepository : Repository<User>, IUsersRepository
    {
        public UsersRepository(DatabaseContext context)
            : base(context)
        {
        }

        private DatabaseContext DatabaseContext => Context as DatabaseContext;

        public async Task<IEnumerable<User>> GetNewUsers(int count)
        {
            return await DatabaseContext.Users.OrderBy(u => u.Id).Take(count).ToListAsync();
        }

        public async Task<User> GetByUserName(string name)
        {
            return await DatabaseContext.Users.FirstOrDefaultAsync(u => u.UserName == name);
        }

        public async Task<List<User>> GetForSearch(string search, int limit = 10)
        {
            var users = await DatabaseContext.Users
                .Where(p => p.AnonimisationDate == null && (p.FirstName.StartsWith(search) || p.Surname.StartsWith(search))).OrderBy(f => f.FirstName).Take(limit).ToListAsync();

            if (users == null || users.Count == 0) users = await DatabaseContext.Users
                    .Where(p => p.AnonimisationDate == null && (p.FirstName.Contains(search) || p.Surname.Contains(search))).OrderBy(f => f.FirstName).Take(limit).ToListAsync();

            return users;
        }

        public async Task<User> GetForPage(long id)
        {
            return await DatabaseContext.Users
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
