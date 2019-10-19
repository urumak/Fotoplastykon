﻿using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class FilmWatchingsRepository : Repository<FilmWatching>, IFilmWatchingsRepository
    {
        public FilmWatchingsRepository(DatabaseContext context)
            : base(context)
        {

        }

        private DatabaseContext DatabaseContext => Context as DatabaseContext;

        public FilmWatching Get(long userId, long filmId)
        {
            return DatabaseContext.FilmWatchings.FirstOrDefault(f => f.FilmId == filmId && f.UserId == userId);
        }
    }
}
