using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.Tools.Pager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public interface IForumService
    {
        Task<IPaginationResult<ForumThread>> GetList(IPager pager);
        Task<IPaginationResult<ForumThread>> GetListForFilm(IPager pager, long filmId);
        Task<IPaginationResult<ForumThread>> GetListForFilmPerson(IPager pager, long filmPersonId);
        Task<ForumThread> Get(long id);
        Task<ForumThreadComment> GetComment(long id);
        Task Add(ForumThread thread, long userId);
        Task Update(long id, ForumThread thread);
        Task Remove(long id);
        Task AddComment(ForumThreadComment comment, long userId);
        Task UpdateComment(long id, ForumThreadComment comment);
        Task RemoveComment(long id);
        Task<bool> CheckIfExists(long id);
        Task<bool> CheckIfCommentExists(long commentId);
    }
}
