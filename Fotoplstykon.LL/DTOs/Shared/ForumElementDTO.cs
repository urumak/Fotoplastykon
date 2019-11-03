using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Fotoplastykon.BLL.DTOs.Shared
{
    public class ForumElementDTO
    {
        public long Id { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string CreatedByName { get; set; }
        public string PhotoUrl { get; set; }
    }
}
