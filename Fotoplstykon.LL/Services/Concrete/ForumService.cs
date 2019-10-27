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
    public class ForumService : Service, IForumService
    {
        public ForumService(IUnitOfWork unit, IMapper mapper)
            : base(unit, mapper)
        {
        }

        public async Task<IPaginationResult<ForumThread>> GetList(IPager pager)
        {
            return await Unit.ForumThreads.GetPaginatedList(pager);
        }

        public async Task<IPaginationResult<ForumThread>> GetListForFilm(IPager pager, long filmId)
        {
            return await Unit.ForumThreads.GetPaginatedList(pager, t => t.FilmId == filmId);
        }

        public async Task<IPaginationResult<ForumThread>> GetListForFilmPerson(IPager pager, long filmPersonId)
        {
            return await Unit.ForumThreads.GetPaginatedList(pager, t => t.PersonId == filmPersonId);
        }

        public async Task<ForumThread> Get(long id)
        {
            return await Unit.ForumThreads.GetWithCommentsAndCreator(id);
        }

        public async Task Add(ForumThread thread, long userId)
        {
            thread.CreatedById = userId;
            await Unit.ForumThreads.Add(thread);
            await Unit.Complete();
        }

        public async Task Update(long id, ForumThread thread)
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

        public async Task AddComment(ForumThreadComment comment, long userId)
        {
            comment.CreatedById = userId;
            await Unit.ForumThreadComments.Add(comment);
            await Unit.Complete();
        }

        public async Task UpdateComment(long id, ForumThreadComment comment)
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
    }
}
