﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.DTOs.Films
{
    public class FilmmakerDTO
    {
        public long PersonId { get; set; }
        public string FullName { get; set; }
        public string RoleType { get; set; }
        public string PhotoUrl { get; set; }
    }
}
