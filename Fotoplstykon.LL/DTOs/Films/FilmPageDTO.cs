using Fotoplastykon.BLL.DTOs.Shared;
using Fotoplastykon.DAL.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Fotoplastykon.BLL.DTOs.Films
{
    public class FilmPageDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public int YearOfProduction { get; set; }
        public decimal Rank { get; set; }
        public string PhotoUrl { get; set; }
        public List<CastMemberDTO> Cast { get; set; }
        public List<FilmmakerDTO> Filmmakers { get; set; }
        public List<ForumElementDTO> ForumThreads { get; set; }
    }
}
