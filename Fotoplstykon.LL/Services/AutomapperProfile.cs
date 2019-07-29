using AutoMapper;
using Fotoplastykon.DAL.Entities.Core;
using Fotoplstykon.LL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplstykon.LL.Services
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<CoreUser, User>();
        }
    }
}
