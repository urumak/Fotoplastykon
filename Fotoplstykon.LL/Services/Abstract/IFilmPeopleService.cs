using Fotoplastykon.BLL.Models.FilmPeople;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public interface IFilmPeopleService
    {
        void Rate(PersonMarkModel mark);
        bool CheckIfWatchingExists(long userId, long personId);
        bool CheckIfExists(long personId);
    }
}
