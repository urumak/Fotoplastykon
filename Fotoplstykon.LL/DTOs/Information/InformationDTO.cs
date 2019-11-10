using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.DTOs.Information
{
    public class InformationDTO
    {
        public long Id { get; set; }
        public string CreatedByName { get; set; }
        public DateTime DateCreated { get; set; }
        public string Title { get; set; }
        public string Introduction { get; set; }
        public string Content { get; set; }
        public string PhotoUrl { get; set; }
        public List<CommentDTO> Comments { get; set; }
    }
}
