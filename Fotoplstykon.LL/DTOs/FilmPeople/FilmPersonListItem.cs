using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.DTOs.FilmPeople
{
    public class FilmPersonListItem
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string PhotoUrl { get; set; }
    }
}
