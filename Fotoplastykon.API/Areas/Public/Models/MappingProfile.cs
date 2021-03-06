﻿using AutoMapper;
using Fotoplastykon.API.Areas.Public.Models;
using Fotoplastykon.BLL.DTOs.Auth;
using Fotoplastykon.BLL.DTOs.Users;
using Fotoplastykon.DAL.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Fotoplastykon.API.Areas.Public.Models.Auth;
using Fotoplastykon.API.Areas.Public.Models.Chat;
using Fotoplastykon.API.Areas.Public.Models.Pages;
using Fotoplastykon.DAL.Enums;
using Fotoplastykon.BLL.Enums;

namespace Fotoplastykon.API.Areas.Public.Models
{
    public class MappingProfile : Profile
    {
        public IConfiguration Configuration { get; set; }
        public MappingProfile(IConfiguration configuration)
        {
            Configuration = configuration;
            UsersMappings();
            ChatMappings();
        }

        private void UsersMappings()
        {
            CreateMap<Auth.RegisterModel, AddUserDTO>();
            CreateMap<TokenDTO, Auth.TokenViewModel>();
            CreateMap<User, Users.ListItemModel>();
            CreateMap<User, Users.UserModel>();
            CreateMap<User, UserProfileModel>()
               .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.PhotoId.HasValue ? Configuration["Files:PublicEndpoint"] + s.PhotoId : string.Empty));
        }

        private void ChatMappings()
        {
            CreateMap<MessageModel, Message>();
            CreateMap<Message, MessageListItemModel>();
        }
    }
}
