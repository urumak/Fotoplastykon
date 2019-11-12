using Fotoplastykon.BLL.DTOs.Forum;
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
        Task<IPaginationResult<ForumListItemDTO>> GetList(IPager pager);
        Task<ForumThreadDTO> Get(long id);
        Task<ForumThreadCommentDTO> GetComment(long id);
        Task<long> Add(ForumThreadDTO thread, long userId);
        Task Update(long id, ForumThreadDTO thread);
        Task Remove(long id);
        Task AddComment(ForumThreadCommentDTO comment, long userId);
        Task UpdateComment(long id, ForumThreadCommentDTO comment);
        Task RemoveComment(long id);
        Task<bool> CheckIfExists(long id);
        Task<bool> CheckIfCommentExists(long commentId);
    }
}
