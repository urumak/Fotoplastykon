using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fotoplastykon.API.Areas.Public.Models.Chat
{
    public class MessageListItemModel
    {
        public long SenderId { get; set; }
        public string MessageText { get; set; }
    }
}
