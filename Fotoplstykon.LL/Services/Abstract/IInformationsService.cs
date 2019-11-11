using Fotoplastykon.BLL.DTOs.Information;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.Tools.Pager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public interface IInformationsService
    {
        Task<IPaginationResult<ListItem>> GetPaginatedList(IPager pager);
        Task<IEnumerable<ListItem>> GetListForMainPage(int limit = 5);
        Task<InformationDTO> GetWithCreatorAndComments(long id);
        Task<InformationComment> GetComment(long id);
        Task<long> AddComment(CommentDTO comment, long userId);
        Task RemoveComment(long id);
        Task UpdateComment(long id, CommentDTO comment);
        Task<bool> CheckIfCommentExists(long id);
        Task<bool> CheckIfExists(long id);
    }
}
