using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Fotoplastykon.BLL.DTOs.Information
{
    public class CommentFormDTO
    {
        public long Id { get; set; }
        public long InformationId { get; set; }
        public long? ParentId { get; set; }
        public string Content { get; set; }
    }
}
