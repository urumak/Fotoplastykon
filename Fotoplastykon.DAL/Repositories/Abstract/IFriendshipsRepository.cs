using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.Tools.InfiniteScroll;
using Fotoplastykon.Tools.Pager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface IFriendshipsRepository : IRepository<Friendship>
    {
        Task<Friendship> Get(long firstId, long secondId);
        Task<Friendship> GetByIvitingAndInvitedId(long invitingId, long invitedId);
        Task<IInfiniteScrollResult<User>> GetListForInfiniteScroll(IInfiniteScroll scroll, long userId);
        Task<List<User>> SearchForFriends(string searchInput, long userId, int limit = 20);
        Task<IPaginationResult<User>> GetPaginedList(IPager pager, long userId);
    }
}
