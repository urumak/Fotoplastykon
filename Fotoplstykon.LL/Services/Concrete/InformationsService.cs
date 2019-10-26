using AutoMapper;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using Fotoplastykon.Tools.Pager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Concrete
{
    public class InformationsService : Service, IInformationsService
    {
        public InformationsService(IUnitOfWork unit, IMapper mapper)
            : base (unit, mapper)
        {
        }

        public async Task<IPaginationResult<Information>> GetPaginatedList(IPager pager)
        {
            return await Unit.Informations.GetPaginatedList(pager);
        }

        public async Task<Information> GetWithCreator(long id)
        {
            return await Unit.Informations.GetWithCreator(id);
        }

        public async Task AddComment(InformationComment comment, long userId)
        {
            comment.CreatedById = userId;
            await Unit.InformationComments.Add(comment);
            await Unit.Complete();
        }
        public async Task RemoveComment(long id)
        {
            await Unit.InformationComments.Remove(id);
            await Unit.Complete();
        }

        public async Task UpdateComment(long id, InformationComment comment)
        {
            var entity = await Unit.InformationComments.Get(id);
            Mapper.Map(comment, entity);
            await Unit.Complete();
        }

        public async Task<bool> CheckIfCommentExists(long id)
        {
            return await Unit.InformationComments.Get(id) != null;
        }

        public async Task<bool> CheckIfExists(long id)
        {
            return await Unit.Informations.Get(id) != null;
        }

        public async Task<List<InformationComment>> GetList(long informationId)
        {
            return Mapper.Map<List<InformationComment>>(await Unit.InformationComments.GetList(informationId));
        }
    }
}
