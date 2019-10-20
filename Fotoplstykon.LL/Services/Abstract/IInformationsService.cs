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
        Task<IPaginationResult<Information>> GetPaginatedList(IPager pager);
        Task<Information> GetWithCreator(long id);
        Task AddComment(InformationComment comment);
        Task RemoveComment(long id);
        Task UpdateComment(long id, InformationComment comment);
        Task<bool> CheckIfCommentExists(long id);
    }
}
