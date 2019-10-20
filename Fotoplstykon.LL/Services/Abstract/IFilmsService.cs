using Fotoplastykon.BLL.Models.Films;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public interface IFilmsService
    {
        Task Rate(FilmMarkModel mark);
        Task<bool> CheckIfWatchingExists(long userId, long filmId);
        Task<bool> CheckIfExists(long filmId);
    }
}
