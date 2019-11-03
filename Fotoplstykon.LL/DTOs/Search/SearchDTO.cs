using Fotoplastykon.BLL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.DTOs.Search
{
    public class SearchDTO
    {
        public string Value { get; set; }
        public long Id { get; set; }
        public string PhotoUrl { get; set; }
        public ItemType Type { get; set; }
    }
}
