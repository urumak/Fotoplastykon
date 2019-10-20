using Fotoplastykon.DAL.Entities.Concrete;
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
    }
}
