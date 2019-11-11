using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.DTOs.Forum
{
    public class ForumListItemDTO
    {
        public long Id { get; set; }
        public long? FilmId { get; set; }
        public long? PersonId { get; set; }
        public string CreatorFullName { get; set; }
        public string PhotoUrl { get; set; }
        public long CreatedById { get; set; }
        public DateTime DateCreated { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
    }
}
