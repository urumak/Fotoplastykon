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
        public decimal Rating { get; set; }
        public decimal? UserRating { get; set; }
        public string PhotoUrl { get; set; }
    }
}
