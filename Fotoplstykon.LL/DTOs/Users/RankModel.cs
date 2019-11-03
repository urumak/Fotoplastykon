using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.DTOs.Users
{
    public class RankModel
    {
        public long Id { get; set; }
        public string ItemName { get; set; }
        public int Mark { get; set; }
        public string PhotoUrl { get; set; }
    }
}
