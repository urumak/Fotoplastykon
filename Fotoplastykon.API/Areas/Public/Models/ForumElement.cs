using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fotoplastykon.API.Areas.Public.Models
{
    public class ForumElement
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string CreatedByName { get; set; }
        public IFormFile UserPhoto { get; set; }
    }
}
