using Fotoplastykon.BLL.DTOs.Shared;
using Fotoplastykon.DAL.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Fotoplastykon.BLL.DTOs.Pages
{
    public class FilmPersonPageDTO
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Profession { get; set; }
        public decimal Rank { get; set; }
        public string PhotoUrl { get; set; }
        public List<RoleInFilmDTO> Roles { get; set; }
        public List<FilmMakingDTO> FilmMakings { get; set; }
        public List<ForumElementDTO> ForumThreads { get; set; }
    }
}
