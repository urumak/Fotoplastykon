using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.DTOs.Forum
{
    public class ForumThreadCommentDTO
    {
        public long Id { get; set; }
        public long ThreadId { get; set; }
        public long? ParentId { get; set; }
        public string CreatedByName { get; set; }
        public string PhotoUrl { get; set; }
        public long CreatedById { get; set; }
        public DateTime DateCreated { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public List<ForumThreadCommentDTO> Replies { get; set; }
    }
}
