using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.DTOs.FilmPeople
{
    public class PersonMarkDTO
    {
        public long UserId { get; set; }
        public long FilmPersonId { get; set; }
        public int Mark { get; set; }
    }
}
