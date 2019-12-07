using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.DTOs.Chat
{
    public class ChatListItemDTO
    {
        public long Id { get; set; }
        public string NameAndSurname { get; set; }
        public string PhotoUrl { get; set; }
    }
}
