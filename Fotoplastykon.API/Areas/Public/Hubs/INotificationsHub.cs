using Fotoplastykon.BLL.DTOs.Messages;
using Fotoplastykon.BLL.DTOs.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fotoplastykon.API.Areas.Public.Hubs
{
    public interface INotificationsHub
    {
        Task ChatMessageReceived(long senderId, MessageDTO message);
        Task NotificationReceived(NotificationDTO notification);
        Task RefreshNotifications(long notificationId);
        Task RefreshChatList();
    }
}
