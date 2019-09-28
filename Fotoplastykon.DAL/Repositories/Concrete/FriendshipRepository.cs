using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class FriendshipRepository : Repository<Friendship>, IFriendshipRepository
    {
        public FriendshipRepository(DatabaseContext context)
            : base(context)
        {
        }
    }
}
