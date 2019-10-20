using Fotoplastykon.BLL.Models.FilmPeople;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public interface IFilmPeopleService
    {
        Task Rate(PersonMarkModel mark);
        Task<bool> CheckIfWatchingExists(long userId, long personId);
        Task<bool> CheckIfExists(long personId);
    }
}
