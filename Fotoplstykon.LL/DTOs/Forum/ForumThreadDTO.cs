using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.DTOs.Forum
{
    public class ForumThreadDTO : ForumListItemDTO
    {
        public List<ForumThreadCommentDTO> Comments { get; set; }
    }
}
