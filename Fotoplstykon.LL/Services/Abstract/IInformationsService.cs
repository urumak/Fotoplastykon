using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.Tools.Pager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public interface IInformationsService
    {
        IPaginationResult<Information> GetPaginatedList(IPager pager);
        Information GetWithCreator(long id);
        void AddComment(InformationComment comment);
        void RemoveComment(long id);
        void UpdateComment(long id, InformationComment comment);
        bool CheckIfCommentExists(long id);
    }
}
