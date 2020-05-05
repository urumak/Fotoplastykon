using AutoMapper;
using Fotoplastykon.BLL.DTOs.Forum;
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
    public class ForumService : Service, IForumService
    {
        public ForumService(IUnitOfWork unit, IMapper mapper)
            : base(unit, mapper)
        {
        }

        public async Task<IPaginationResult<ForumListItemDTO>> GetList(IPager pager)
        {
            var data = await Unit.ForumThreads.GetPaginatedList(pager, t => t.CreatedBy, t => t.DateCreated, DAL.Enums.OrderDirection.DESC);
            return new PaginationResult<ForumListItemDTO>
            {
                Items = Mapper.Map<List<ForumListItemDTO>>(data.Items),
                Pager = data.Pager
            };
        }

        public async Task<ForumThreadDTO> Get(long id)
        {
            var thread = Mapper.Map<ForumThreadDTO>(await Unit.ForumThreads.GetWithCreator(id));
            var test = await Unit.ForumThreadComments.GetList(id);
            thread.Comments = Mapper.Map<List<ForumThreadCommentDTO>>(await Unit.ForumThreadComments.GetList(id));

            return thread;
        }

        public async Task<long> AddForFilm(ForumThreadDTO thread, long userId, long filmId)
        {
            var entity = Mapper.Map<ForumThread>(thread);
            entity.CreatedById = userId;
            entity.FilmId = filmId;
            entity = await Unit.ForumThreads.Add(entity);
            await Unit.Complete();

            return entity.Id;
        }

        public async Task<long> AddForFilmPerson(ForumThreadDTO thread, long userId, long filmPersonId)
        {
            var entity = Mapper.Map<ForumThread>(thread);
            entity.CreatedById = userId;
            entity.PersonId = filmPersonId;
            entity = await Unit.ForumThreads.Add(entity);
            await Unit.Complete();

            return entity.Id;
        }

        public async Task<long> Add(ForumThreadDTO thread, long userId)
        {
            var entity = Mapper.Map<ForumThread>(thread);
            entity.CreatedById = userId;
            entity = await Unit.ForumThreads.Add(entity);
            await Unit.Complete();

            return entity.Id;
        }

        public async Task Update(long id, ForumThreadDTO thread)
        {
            var entity = await Unit.ForumThreads.Get(id);
            Mapper.Map(thread, entity);
            await Unit.Complete();
        }

        public async Task Remove(long id)
        {
            await Unit.ForumThreads.Remove(id);
            await Unit.Complete();
        }

        public async Task AddComment(ForumThreadCommentDTO comment, long userId)
        {
            var entity = Mapper.Map<ForumThreadComment>(comment);
            entity.CreatedById = userId;
            await Unit.ForumThreadComments.Add(entity);
            await Unit.Complete();
        }

        public async Task UpdateComment(long id, ForumThreadCommentDTO comment)
        {
            var entity = await Unit.ForumThreadComments.Get(id);
            Mapper.Map(comment, entity);
            await Unit.Complete();
        }

        public async Task RemoveComment(long id)
        {
            await Unit.ForumThreadComments.Remove(id);
            await Unit.Complete();
        }

        public async Task<bool> CheckIfExists(long id)
        {
            return await Unit.ForumThreads.Get(id) != null;
        }

        public async Task<bool> CheckIfCommentExists(long commentId)
        {
            return await Unit.ForumThreadComments.Get(commentId) != null;
        }

        public async Task<ForumThreadCommentDTO> GetComment(long id)
        {
            return Mapper.Map<ForumThreadCommentDTO>(await Unit.ForumThreadComments.Get(id));
        }
    }
}
