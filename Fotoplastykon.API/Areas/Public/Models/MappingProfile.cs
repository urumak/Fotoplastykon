﻿using AutoMapper;
using Fotoplastykon.API.Areas.Public.Models.Auth;
using Fotoplastykon.BLL.Models.Auth;
using Fotoplastykon.BLL.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fotoplastykon.API.Areas.Public.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            AddAuthMappings();
        }

        void AddAuthMappings()
        {
            CreateMap<RegisterModel, AddUserModel>();
            CreateMap<TokenModel, TokenViewModel>();
        }
    }
}