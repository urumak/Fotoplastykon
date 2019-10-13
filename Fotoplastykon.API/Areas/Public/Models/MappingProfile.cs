using AutoMapper;
using Fotoplastykon.API.Areas.Public.Models;
using Fotoplastykon.BLL.Models.Auth;
using Fotoplastykon.BLL.Models.Users;
using Fotoplastykon.DAL.Entities.Concrete;
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
            UsersMappings();
            InformationsMappings();
        }

        void UsersMappings()
        {
            CreateMap<Auth.RegisterModel, AddUserModel>();
            CreateMap<TokenModel, Auth.TokenViewModel>();
            CreateMap<User, Users.ListItemModel>();
        }

        void InformationsMappings()
        {
            CreateMap<Information, Informations.ListItemModel>();
            CreateMap<Information, Informations.InformationModel>()
                .ForMember(d => d.CreatedByName, o => o.MapFrom(s => s.CreatedBy.FirstName + " " + s.CreatedBy.Surname));
        }
    }
}
