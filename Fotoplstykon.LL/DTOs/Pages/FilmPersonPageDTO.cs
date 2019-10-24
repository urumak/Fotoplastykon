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
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Profession { get; set; }
        public decimal Rank { get; set; }
        public string FilePath { get; set; }
        public FileInfo Photo { get; set; }
        public List<PersonInRoleDTO> Roles { get; set; }
        public List<ForumElementDTO> ForumThreads { get; set; }
    }
}
