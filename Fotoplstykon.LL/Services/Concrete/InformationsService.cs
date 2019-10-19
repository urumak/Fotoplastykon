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
    public class InformationsService : Service, IInformationsService
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

        public void AddComment(InformationComment comment)
        {
            Unit.InformationComments.Add(comment);
            Unit.Complete();
        }
        public void RemoveComment(long id)
        {
            Unit.InformationComments.Remove(id);
            Unit.Complete();
        }

        public void UpdateComment(long id, InformationComment comment)
        {
            var entity = Unit.InformationComments.Get(id);
            Mapper.Map(comment, entity);
            Unit.Complete();
        }

        public bool CheckIfCommentExists(long id)
        {
            return Unit.InformationComments.Get(id) != null;
        }
    }
}
