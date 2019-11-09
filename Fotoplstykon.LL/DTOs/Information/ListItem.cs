using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.DTOs.Information
{
    public class ListItem
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Introduction { get; set; }
        public string PhotoUrl { get; set; }
    }
}
