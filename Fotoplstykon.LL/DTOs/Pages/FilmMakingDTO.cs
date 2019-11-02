using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.DTOs.Pages
{
    public class FilmMakingDTO
    {
        public string PersonPublicId { get; set; }
        public string FilmName { get; set; }
        public string Role { get; set; }
        public string YearOfProduction { get; set; }
        public string PhotoUrl { get; set; }
    }
}
