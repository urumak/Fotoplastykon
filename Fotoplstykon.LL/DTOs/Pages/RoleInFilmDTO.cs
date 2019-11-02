using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.DTOs.Pages
{
    public class RoleInFilmDTO
    {
        public string FilmPublicId { get; set; }
        public string FilmName { get; set; }
        public string CharacterName { get; set; }
        public int YearOfProduction { get; set; }
        public string PhotoUrl { get; set; }
    }
}
