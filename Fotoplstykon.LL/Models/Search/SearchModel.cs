using Fotoplastykon.BLL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.Models.Search
{
    public class SearchModel
    {
        public string Value { get; set; }
        public string Key { get; set; }
        public SearchItemType Type { get; set; }
    }
}
