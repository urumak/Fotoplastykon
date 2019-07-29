using AutoMapper;
using Fotoplstykon.LL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Fotoplastykon.DAL.Entities.Concrete.Core;

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
