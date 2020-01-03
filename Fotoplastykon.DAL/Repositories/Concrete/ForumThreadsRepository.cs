using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using Fotoplastykon.Tools.Pager;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class ForumThreadsRepository : Repository<ForumThread>, IForumThreadsRepository
    {
        public ForumThreadsRepository(DatabaseContext context)
            : base(context)
        {
        }

        private DatabaseContext DatabaseContext => Context as DatabaseContext;

        public async Task<ForumThread> GetWithCreator(long id)
        {
            return await DatabaseContext.ForumThreads
                .Include(t => t.CreatedBy)
                .Include(t => t.Comments)
                .ThenInclude(c => c.Replies)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IPaginationResult<ForumThread>> GetTheMostPopularForFilm(IPager pager, long filmId)
        {
            var predicate = PredicateBuilder.New<ForumThread>(t => t.FilmId == filmId);

            if (!string.IsNullOrEmpty(pager.Search)) predicate.And(t => t.Subject.Contains(pager.Search));

            return await DatabaseContext.ForumThreads
                .Include(t => t.CreatedBy)
                .Include(t => t.Comments)
                .ThenInclude(c => c.Replies)
                .Where(predicate)
                .OrderByDescending(t => t.Comments.Select(c => c.Replies).Count() + t.Comments.Count())
                .GetPaginationResult(pager);
        }

        public async Task<IPaginationResult<ForumThread>> GetTheMostPopularForFilmPerson(IPager pager, long personId)
        {
            var predicate = PredicateBuilder.New<ForumThread>(t => t.PersonId == personId);

            if (!string.IsNullOrEmpty(pager.Search)) predicate.And(t => t.Subject.Contains(pager.Search));

            return await DatabaseContext.ForumThreads
                .Include(t => t.CreatedBy)
                .Include(t => t.Comments)
                .ThenInclude(c => c.Replies)
                .Where(predicate)
                .OrderByDescending(t => t.Comments.Select(c => c.Replies).Count() + t.Comments.Count())
                .GetPaginationResult(pager);
        }
    }
}
