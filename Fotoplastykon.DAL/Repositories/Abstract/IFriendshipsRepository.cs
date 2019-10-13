using Fotoplastykon.DAL.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface IFriendshipsRepository : IRepository<Friendship>
    {
        Friendship Get(long firstId, long secondId);
        Friendship GetByIvitingAndInvitedId(long invitingId, long invitedId);
    }
}
