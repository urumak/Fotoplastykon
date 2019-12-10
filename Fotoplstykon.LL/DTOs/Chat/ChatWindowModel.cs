using Fotoplastykon.BLL.DTOs.Messages;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.Tools.InfiniteScroll;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.DTOs.Chat
{
    public class ChatWindowModel
    {
        public long Id { get; set; }
        public string NameAndSurname { get; set; }
        public string PhotoUrl { get; set; }
        public IInfiniteScrollResult<MessageDTO> Messages { get; set; }
    }
}
