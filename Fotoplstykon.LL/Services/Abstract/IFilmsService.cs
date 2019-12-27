using Fotoplastykon.BLL.DTOs.Films;
using Fotoplastykon.BLL.DTOs.Users;
using Fotoplastykon.Tools.Pager;
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
        Task<FilmPageDTO> GetForPage(long filmId, long userId);
        Task<decimal?> GetRating(long filmId);
        Task<IPaginationResult<RankModel>> GetPaginatedListForUser(IPager pager, long userId);
    }
}
