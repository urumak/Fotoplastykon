using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fotoplastykon.API.Areas.Public.Models.Pages
{
    public class FilmPersonPageModel
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Profession { get; set; }
        public decimal Rank { get; set; }
        public string PhotoUrl { get; set; }
        public List<PersonInRoleModel> Roles { get; set; }
        public List<ForumElement> ForumThreads { get; set; }
    }
}
