using Fotoplastykon.BLL.DTOs.Shared;
using Fotoplastykon.DAL.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Fotoplastykon.BLL.DTOs.Pages
{
    public class FilmPageDTO
    {
        public string Title { get; set; }
        public int YearOfProduction { get; set; }
        public decimal Rank { get; set; }
        public string FilePath { get; set; }
        public FileInfo Photo { get; set; }
        public List<LinkedItemDTO> Cast { get; set; }
        public List<LinkedItemDTO> Filmmakers { get; set; }
        public List<ForumElementDTO> ForumThreads { get; set; }
        public List<LinkedItemDTO> PageCocreators { get; set; }
    }
}
