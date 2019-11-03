using Fotoplastykon.BLL.DTOs.Films;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public interface IFilmsService
    {
        Task Rate(FilmMarkDTO mark);
        Task<bool> CheckIfWatchingExists(long userId, long filmId);
        Task<bool> CheckIfExists(long filmId);
        Task<FilmPageDTO> GetForPage(long id);
        Task<int?> GetRate(long userId, long filmId);
    }
}
