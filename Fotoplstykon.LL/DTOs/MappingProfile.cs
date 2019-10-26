using System.Linq;
using AutoMapper;
using Fotoplastykon.BLL.Enums;
using Fotoplastykon.BLL.DTOs.FilmPeople;
using Fotoplastykon.BLL.DTOs.Films;
using Fotoplastykon.BLL.DTOs.Quizes;
using Fotoplastykon.BLL.DTOs.Search;
using Fotoplastykon.BLL.DTOs.Users;
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
            SearchMappings();
        }

        private void UserMappings()
        {
            CreateMap<User, AddUserDTO>()
                .ReverseMap()
                .ForMember(d => d.PasswordHash, o => o.Ignore());
        }

        private void QuizMappings()
        {
            CreateMap<Quiz, QuizResultDTO>();
            CreateMap<QuizQuestion, QuestionResultDTO>();
            CreateMap<QuizAnswer, AnswerResultDTO>();
        }

        private void FilmMappings()
        {
            CreateMap<FilmMarkDTO, FilmWatching>();
        }

        private void FilmPersonMappings()
        {
            CreateMap<PersonMarkDTO, PersonMark>();
        }

        private void SearchMappings()
        {
            CreateMap<User, SearchDTO>()
                .ForMember(d => d.Value, o => o.MapFrom(s => s.FirstName + " " + s.Surname))
                .ForMember(d => d.Key, o => o.MapFrom(s => s.PublicId))
                .ForMember(d => d.Type, o => o.MapFrom(s => ItemType.User));

            CreateMap<Film, SearchDTO>()
                .ForMember(d => d.Value, o => o.MapFrom(s => s.Title))
                .ForMember(d => d.Key, o => o.MapFrom(s => s.PagePublicId))
                .ForMember(d => d.Type, o => o.MapFrom(s => ItemType.Film));

            CreateMap<FilmPerson, SearchDTO>()
                .ForMember(d => d.Value, o => o.MapFrom(s => s.FirstName + " " + s.Surname))
                .ForMember(d => d.Key, o => o.MapFrom(s => s.PagePublicId))
                .ForMember(d => d.Type, o => o.MapFrom(s => ItemType.FilmPerson));
        }
    }
}
