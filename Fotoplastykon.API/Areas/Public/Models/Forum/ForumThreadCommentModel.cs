using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fotoplastykon.API.Areas.Public.Models.Forum
{
    public class ForumThreadCommentModel
    {
        public long Id { get; set; }
        public long ForumThreadId { get; set; }
        public long? ParentId { get; set; }
        public string CreatorFullName { get; set; }
        public DateTime DateCreated { get; set; }
        public string Content { get; set; }
        public List<ForumThreadCommentModel> Replies { get; set; }
    }
}
