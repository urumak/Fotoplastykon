using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class NotificationsReadingsRepository : Repository<NotificationsReading>, INotificationsReadingsRepository
    {
        public NotificationsReadingsRepository(DatabaseContext context)
            : base(context)
        {
        }
        private DatabaseContext DatabaseContext => Context as DatabaseContext;
    }
}
