using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fotoplastykon.API.Areas.Public.Models.Informations
{
    public class CommentModel
    {
        public long Id { get; set; }
        public string CreatorFullName { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public List<CommentModel> Replies { get; set; }
    }
}
