using Fotoplastykon.BLL.DTOs.Films;
using Fotoplastykon.BLL.DTOs.Shared;
using Fotoplastykon.BLL.DTOs.Users;
using Fotoplastykon.Tools.Pager;
using Microsoft.AspNetCore.Http;
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
        Task<IPaginationResult<FilmListItem>> GetPaginatedList(IPager pager);
        Task Remove(long id);
        Task ChangePhoto(long id, IFormFile file);
        Task<FilmFormModel> Fetch(long id);
        Task<List<FilmPersonDropDownModel>> GetFilmmakers(string search, int limit = 10);
        List<RoleTypeDictionary> GetRoleTypes();
        Task<FilmPersonDropDownModel> GetForSearch(long id);
        Task Update(long id, FilmFormModel model);
        Task<IPaginationResult<CastMemberDTO>> GetFilmCast(IPager pager, long filmId);
        Task<IPaginationResult<FilmmakerDTO>> GetFilmMakers(IPager pager, long filmId);
        Task<IPaginationResult<ForumElementDTO>> GetMostPolularForumThreads(IPager pager, long filmId);
    }
}
