using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;
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
    }
}
