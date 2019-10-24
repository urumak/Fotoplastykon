using System.Linq;
using AutoMapper;
using Fotoplastykon.BLL.Enums;
using Fotoplastykon.BLL.Models.FilmPeople;
using Fotoplastykon.BLL.Models.Films;
using Fotoplastykon.BLL.Models.Quizes;
using Fotoplastykon.BLL.Models.Search;
using Fotoplastykon.BLL.Models.Users;
using Fotoplastykon.DAL.Entities.Concrete;

namespace Fotoplastykon.BLL.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            UserMappings();
            QuizMappings();
            FilmMappings();
            FilmPersonMappings();
            InformationMappings();
            SearchMappings();
        }

        private void UserMappings()
        {
            CreateMap<User, AddUserModel>()
                .ReverseMap()
                .ForMember(d => d.PasswordHash, o => o.Ignore());
        }

        private void QuizMappings()
        {
            CreateMap<Quiz, QuizResultModel>();
            CreateMap<QuizQuestion, QuestionResultModel>();
            CreateMap<QuizAnswer, AnswerResultModel>();
        }

        private void FilmMappings()
        {
            CreateMap<FilmMarkModel, FilmWatching>();
        }

        private void FilmPersonMappings()
        {
            CreateMap<PersonMarkModel, PersonMark>();
        }

        private void InformationMappings()
        {
            CreateMap<InformationComment, InformationComment>()
                .ForMember(d => d.Id, o => o.Ignore());
        }

        private void SearchMappings()
        {
            CreateMap<User, SearchModel>()
                .ForMember(d => d.Value, o => o.MapFrom(s => s.FirstName + " " + s.Surname))
                .ForMember(d => d.Key, o => o.MapFrom(s => s.PublicId))
                .ForMember(d => d.Type, o => o.MapFrom(s => SearchItemType.User));

            CreateMap<Film, SearchModel>()
                .ForMember(d => d.Value, o => o.MapFrom(s => s.Title))
                .ForMember(d => d.Key, o => o.MapFrom(s => s.PagePublicId))
                .ForMember(d => d.Type, o => o.MapFrom(s => SearchItemType.User));

            CreateMap<FilmPerson, SearchModel>()
                .ForMember(d => d.Value, o => o.MapFrom(s => s.FirstName + " " + s.Surname))
                .ForMember(d => d.Key, o => o.MapFrom(s => s.PagePublicId))
                .ForMember(d => d.Type, o => o.MapFrom(s => SearchItemType.User));
        }
    }
}
