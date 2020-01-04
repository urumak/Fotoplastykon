using Fotoplastykon.BLL.DTOs.FilmPeople;
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
    public interface IFilmPeopleService
    {
        Task Rate(PersonMarkDTO mark);
        Task<bool> CheckIfWatchingExists(long userId, long personId);
        Task<bool> CheckIfExists(long personId);
        Task<FilmPersonPageDTO> GetForPage(long personId, long userId);
        Task<decimal?> GetRating(long personId);
        Task<IPaginationResult<RankModel>> GetPaginatedListForUser(IPager pager, long userId);
        Task<IPaginationResult<FilmPersonListItem>> GetList(IPager pager);
        Task ChangePhoto(long id, IFormFile file);
        Task Remove(long id);
        Task<FilmPersonFormModel> Fetch(long id);
        Task Update(long id, FilmPersonFormModel model);
        Task<IPaginationResult<ForumElementDTO>> GetTheMostPopularForumThreads(IPager pager, long personId);
        Task<IPaginationResult<RoleInFilmDTO>> GetPersonRoles(IPager pager, long personId);
        Task<long> Add(FilmPersonFormModel model);
    }
}
