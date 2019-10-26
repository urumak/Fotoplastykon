using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Fotoplastykon.BLL.DTOs.Shared
{
    public class ForumElementDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string CreatedByName { get; set; }
        public long PhotoId { get; set; }
    }
}
