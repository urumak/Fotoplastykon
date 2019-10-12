using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.Tools.Pager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public interface IInformationSerivice
    {
        IPaginationResult<Information> GetPaginatedList(IPager pager);
    }
}
