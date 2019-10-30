using Fotoplastykon.BLL.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fotoplastykon.API.Areas.Public.Models
{
    public class LinkedItemModel
    {
        public string Value { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }
        public long PhotoId { get; set; }
        public ItemType Type { get; set; }
    }
}
