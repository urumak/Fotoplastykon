using AutoMapper;
using Fotoplastykon.BLL.DTOs.Notifications;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Enums;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using Fotoplastykon.Tools.InfiniteScroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Concrete
{
    public class NotificationsService : Service, INotificationsService
    {
        public NotificationsService(IUnitOfWork unit, IMapper mapper)
            : base(unit, mapper)
        {
        }


        public async Task<IInfiniteScrollResult<NotificationDTO>> GetNotifications(IInfiniteScroll scroll, long userId)
        {
            var reading = await Unit.NotificationsReadings.Get(r => r.UserId == userId);
            var invitersIds = (await Unit.Invitations.Find(r => r.InvitedId == userId)).Select(x => x.InvitingId);
            var data = await Unit.InvitationNotifications.GetListForInfiniteScroll(scroll, n => n.UserId == userId, n => n.Friend, n => n.DateCreated, OrderDirection.DESC);

            var items = Mapper.Map<List<NotificationDTO>>(data.Items);

            items.ForEach(i =>
            {
                i.CanAccept = invitersIds.Contains(i.FriendId);
                i.Unread = reading == null ? true : reading.LastReadingDate < i.DateCreated;
            });

            return new InfiniteScrollResult<NotificationDTO>
            {
                Items = items,
                Scroll = data.Scroll
            };
        }

        public async Task<int> GetNotificationsCount(long userId)
        {
            var reading = await Unit.NotificationsReadings.Get(r => r.UserId == userId);

            return (await Unit.InvitationNotifications.Find(x => x.UserId == userId
                && (reading == null || x.DateCreated > reading.LastReadingDate)))
                .Count();
        }

        public async Task ReadMessages(long userId)
        {
            var reading = await Unit.NotificationsReadings.Get(r => r.UserId == userId);

            if (reading == null)
            {
                await Unit.NotificationsReadings.Add(new NotificationsReading
                {
                    UserId = userId,
                    LastReadingDate = DateTime.Now
                });
            }
            else
            {
                reading.LastReadingDate = DateTime.Now;
            }

            await Unit.Complete();
        }
    }
}
