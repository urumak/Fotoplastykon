using Fotoplastykon.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.DTOs.Notifications
{
    public class NotificationDTO
    {
        public long Id { get; set; }
        public long FriendId { get; set; }
        public string NameAndSurname { get; set; }
        public bool Unread { get; set; }
        public bool CanAccept { get; set; }
        public string PhotoUrl { get; set; }
        public NotificationType Type { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
