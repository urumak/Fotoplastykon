using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fotoplastykon.API.Areas.Public.Models.Pages
{
    public class FilmPageModel
    {
        public string Title { get; set; }
        public int YearOfProduction { get; set; }
        public decimal Rank { get; set; }
        public List<LinkedItemModel> Cast { get; set; }
        public List<LinkedItemModel> Filmmakers { get; set; }
        public List<ForumElement> ForumThreads { get; set; }
    }
}
