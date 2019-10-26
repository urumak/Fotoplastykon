using AutoMapper;
using Fotoplastykon.API.Areas.Public.Models;
using Fotoplastykon.BLL.DTOs.Auth;
using Fotoplastykon.BLL.DTOs.Users;
using Fotoplastykon.DAL.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Fotoplastykon.API.Areas.Public.Models
{
    public class MappingProfile : Profile
    {
        public IConfiguration Configuration { get; set; }
        public MappingProfile(IConfiguration configuration)
        {
            Configuration = configuration;
            UsersMappings();
            InformationsMappings();
            QuizesMappings();
        }

        void UsersMappings()
        {
            CreateMap<Auth.RegisterModel, AddUserDTO>();
            CreateMap<TokenDTO, Auth.TokenViewModel>();
            CreateMap<User, Users.ListItemModel>();
            CreateMap<User, Users.UserModel>();
        }

        void InformationsMappings()
        {
            CreateMap<Information, Informations.ListItemModel>();
            CreateMap<Information, Informations.InformationModel>()
                .ForMember(d => d.CreatedByName, o => o.MapFrom(s => s.CreatedBy.FirstName + " " + s.CreatedBy.Surname));

            CreateMap<InformationComment, Informations.CommentModel>()
                .ForMember(d => d.CreatorFullName, o => o.MapFrom(s => s.CreatedBy.FirstName + " " + s.CreatedBy.Surname))
                .ForMember(d => d.Replies, o => o.MapFrom(s => s.Replies))
                .AfterMap((s, d) =>
                {
                    if (s.DateDeleted.HasValue) d.Content = Configuration["DeletedItems:UserDescription"];
                    if(s.CreatedBy.AnonimisationDate.HasValue) d.CreatorFullName = Configuration["DeletedItems:ItemDescription"];
                });

            CreateMap<Informations.CommentFormModel, Information>();
        }

        void QuizesMappings()
        {
            CreateMap<Quiz, Quizes.QuizModel>();
            CreateMap<QuizQuestion, Quizes.QuestionModel>();
            CreateMap<QuizAnswer, Quizes.AnswerModel>();
        }
    }
}
