using Fotoplastykon.BLL.DTOs.Notifications;
using Fotoplastykon.Tools.InfiniteScroll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public interface INotificationsService
    {
        Task<IInfiniteScrollResult<NotificationDTO>> GetNotifications(IInfiniteScroll scroll, long userId);
        Task<int> GetNotificationsCount(long userId);
        Task ReadMessages(long userId);
        Task SetDecisionAccepted(long userId, long friendId);
        Task SetDecisionRefused(long userId, long friendId);
    }
}
