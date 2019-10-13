using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.Tools.Pager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public interface IInformationsSerivice
    {
        IPaginationResult<Information> GetPaginatedList(IPager pager);
        Information GetWithCreator(long id);
    }
}
