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
    public class InformationsService : Service, IInformationsSerivice
    {
        public InformationsService(IUnitOfWork unit, IMapper mapper)
            : base (unit, mapper)
        {
        }

        public IPaginationResult<Information> GetPaginatedList(IPager pager)
        {
            return Unit.Informations.GetPaginatedList(pager);
        }

        public Information GetWithCreator(long id)
        {
            return Unit.Informations.GetWithCreator(id);
        }
    }
}
