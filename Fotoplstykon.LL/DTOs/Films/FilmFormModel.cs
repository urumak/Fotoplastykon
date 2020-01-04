using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.DTOs.Films
{
    public class FilmFormModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public int YearOfProduction { get; set; }
        public string PhotoUrl { get; set; }
        public List<PersonInRoleFormModel> People { get; set; }
    }
}
