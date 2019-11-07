using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.DTOs.FilmPeople
{
    public class RoleInFilmDTO
    {
        public long FilmId { get; set; }
        public string FilmName { get; set; }
        public string RoleDescription { get; set; }
        public int YearOfProduction { get; set; }
        public string PhotoUrl { get; set; }
    }
}
