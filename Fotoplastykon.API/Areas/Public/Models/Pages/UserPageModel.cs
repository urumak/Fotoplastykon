using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fotoplastykon.API.Areas.Public.Models.Pages
{
    public class UserPageModel
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public List<LinkedItemModel> WatchedFilms { get; set; }
        public List<LinkedItemModel> RankedPeople { get; set; }
    }
}
