using Fotoplastykon.BLL.Models.Films;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public interface IFilmsService
    {
        void Rate(FilmMarkModel mark);
        bool CheckIfWatchingExists(long userId, long filmId);
        bool CheckIfExists(long filmId);
    }
}
