using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.DTOs.Information
{
    public class CommentDTO
    {
        public long Id { get; set; }
        public string CreatorFullName { get; set; }
        public long CreatedById { get; set; }
        public string PhotoUrl { get; set; }
        public string Content { get; set; }
        public long InformationId { get; set; }
        public long? ParentId { get; set; }
        public DateTime DateCreated { get; set; }
        public List<CommentDTO> Replies { get; set; }
    }
}
