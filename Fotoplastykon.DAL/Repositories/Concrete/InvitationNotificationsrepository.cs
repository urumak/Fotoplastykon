using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class InvitationNotificationRepository : Repository<InvitationNotification>, IInvitationNotificationsRepository
    {
        public InvitationNotificationRepository(DatabaseContext context)
            : base(context)
        {
        }
        private DatabaseContext DatabaseContext => Context as DatabaseContext;

        public async Task<InvitationNotification> GetLast(long userId, long friendId)
        {
            return await DatabaseContext.InvitationNotifications.OrderByDescending(x => x.DateCreated)
                .FirstOrDefaultAsync(i => i.FriendId == userId && i.UserId == friendId);
        }
    }
}
