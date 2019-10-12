using AutoMapper;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using Fotoplastykon.Tools.Pager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.Services.Concrete
{
    public class InformationService : Service, IInformationSerivice
    {
        public InformationService(IUnitOfWork unit, IMapper mapper)
            : base (unit, mapper)
        {
        }

        public IPaginationResult<Information>  GetPaginatedList(IPager pager)
        {
            return Unit.Informations.GetPaginatedList(pager);
        }
    }
}
