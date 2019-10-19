﻿using Fotoplastykon.DAL.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface IFilmWatchingsRepository : IRepository<FilmWatching>
    {
        FilmWatching Get(long userId, long filmId);
    }
}
