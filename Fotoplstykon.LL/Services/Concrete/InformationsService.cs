using AutoMapper;
using Fotoplastykon.BLL.DTOs.Information;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Enums;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using Fotoplastykon.Tools.Pager;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IPaginationResult<ListItem>> GetPaginatedList(IPager pager)
        {
            var data = await Unit.Informations.GetPaginatedList(pager, i => i.DateCreated, OrderDirection.DESC);
            return new PaginationResult<ListItem>
            {
                Items = Mapper.Map<List<ListItem>>(data.Items),
                Pager = data.Pager
            };
        }

        public async Task<IEnumerable<ListItem>> GetListForMainPage(int limit = 5)
        {
            return Mapper.Map<IEnumerable<ListItem>>(await Unit.Informations.GetForMainPage(limit));
        }

        public async Task<InformationDTO> GetWithCreatorAndComments(long id)
        {
            var information = Mapper.Map<InformationDTO>(await Unit.Informations.GetWithCreator(id));
            information.Comments = Mapper.Map<List<CommentDTO>>(await Unit.InformationComments.GetList(id));

            return information;
        }

        public async Task<long> AddComment(CommentDTO comment, long userId)
        {
            var entity = Mapper.Map<InformationComment>(comment);
            entity.CreatedById = userId;
            entity = await Unit.InformationComments.Add(entity);
            await Unit.Complete();

            return entity.Id;
        }
        public async Task RemoveComment(long id)
        {
            await Unit.InformationComments.Remove(id);
            await Unit.Complete();
        }

        public async Task UpdateComment(long id, CommentDTO comment)
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

        public async Task<List<InformationComment>> GetComments(long informationId)
        {
            return Mapper.Map<List<InformationComment>>(await Unit.InformationComments.GetList(informationId));
        }

        public async Task<InformationComment> GetComment(long id)
        {
            return await Unit.InformationComments.Get(id);
        }
    }
}
