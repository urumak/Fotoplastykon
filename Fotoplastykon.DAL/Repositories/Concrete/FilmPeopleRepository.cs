﻿using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using Fotoplastykon.Tools.Pager;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class FilmPeopleRepository : Repository<FilmPerson>, IFilmPeopleRepository
    {
        public FilmPeopleRepository(DatabaseContext context)
            : base(context)
        {
        }

        private DatabaseContext DatabaseContext => Context as DatabaseContext;

        public async Task<List<FilmPerson>> GetForSearch(string search, int limit = 10)
        {
            var people = await DatabaseContext.FilmPeople
                .Where(p => p.FirstName.StartsWith(search) || p.Surname.StartsWith(search)).OrderBy(f => f.FirstName).Take(limit).ToListAsync();

            if (people == null || people.Count == 0) people = await DatabaseContext.FilmPeople
                    .Where(p => p.FirstName.Contains(search) || p.Surname.Contains(search)).OrderBy(f => f.FirstName).Take(limit).ToListAsync();

            return people;
        }

        public async Task<FilmPerson> GetForPage(long id)
        {
            return await DatabaseContext.FilmPeople
                .Include(u => u.Roles)
                .ThenInclude(r => r.Film)
                .Include(u => u.ForumThreads)
                .Include(u => u.Marks)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<FilmPerson> GetForSearch(long id)
        {
            return await DatabaseContext.FilmPeople.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
