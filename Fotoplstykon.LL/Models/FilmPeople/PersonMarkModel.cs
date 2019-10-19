using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.Models.FilmPeople
{
    public class PersonMarkModel
    {
        public long UserId { get; set; }
        public long FilmPersonId { get; set; }
        public int Mark { get; set; }
    }
}
