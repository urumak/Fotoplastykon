using System.Linq;
using AutoMapper;
using Fotoplastykon.BLL.Enums;
using Fotoplastykon.BLL.DTOs.FilmPeople;
using Fotoplastykon.BLL.DTOs.Films;
using Fotoplastykon.BLL.DTOs.Quizes;
using Fotoplastykon.BLL.DTOs.Search;
using Fotoplastykon.BLL.DTOs.Users;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.BLL.DTOs.Files;
using Microsoft.Extensions.Configuration;
using Fotoplastykon.BLL.DTOs.Shared;
using Fotoplastykon.DAL.Enums;

namespace Fotoplastykon.BLL.Models
{
    public class MappingProfile : Profile
    {
        public IConfiguration Configuration { get; set; }
        public MappingProfile(IConfiguration configuration)
        {
            Configuration = configuration;
            UserMappings();
            QuizMappings();
            FilmMappings();
            FilmPersonMappings();
            SearchMappings();
            FilesMappings();
            SharedMappings();
        }

        private void UserMappings()
        {
            CreateMap<User, AddUserDTO>()
                .ReverseMap()
                .ForMember(d => d.PasswordHash, o => o.Ignore());

            CreateMap<User, UserPageDTO>()
                .ForMember(d => d.WatchedFilms, o => o.MapFrom(s => s.FilmsWatched))
                .ForMember(d => d.RatedPeople, o => o.MapFrom(s => s.RatedPeople))
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.PhotoId.HasValue ? Configuration["Files:PublicEndpoint"] + s.PhotoId : string.Empty));

            CreateMap<FilmWatching, RankModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Film.Id))
                .ForMember(d => d.ItemName, o => o.MapFrom(s => s.Film.Title))
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.Film.PhotoId.HasValue ? Configuration["Files:PublicEndpoint"] + s.Film.PhotoId : string.Empty));

            CreateMap<PersonMark, RankModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Person.Id))
                .ForMember(d => d.ItemName, o => o.MapFrom(s => s.Person.FirstName + " " + s.Person.Surname))
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.Person.PhotoId.HasValue ? Configuration["Files:PublicEndpoint"] + s.Person.PhotoId : string.Empty));
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

            CreateMap<Film, FilmPageDTO>()
                .ForMember(d => d.Filmmakers, o => o.MapFrom(s => s.PeopleInRoles.Where(x => x.Role != RoleType.Actor)))
                .ForMember(d => d.Cast, o => o.MapFrom(s => s.PeopleInRoles.Where(x => x.Role == RoleType.Actor)))
                .ForMember(d => d.Rank, o => o.MapFrom(s => s.Watchings.Select(x => x.Mark).Average()))
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.PhotoId.HasValue ? Configuration["Files:PublicEndpoint"] + s.PhotoId : string.Empty));

            CreateMap<PersonInRole, CastMemberDTO>()
                .ForMember(d => d.FullName, o => o.MapFrom(s => s.Person.FirstName + " " + s.Person.Surname))
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.Person.PhotoId.HasValue ? Configuration["Files:PublicEndpoint"] + s.Person.PhotoId : string.Empty));

            CreateMap<PersonInRole, FilmmakerDTO>()
                .ForMember(d => d.PersonPublicId, o => o.MapFrom(s => s.Person.PublicId))
                .ForMember(d => d.FullName, o => o.MapFrom(s => s.Person.FirstName + " " + s.Person.Surname))
                .ForMember(d => d.Profession, o => o.MapFrom(s => s.Person.Profession.ToString()))
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.Person.PhotoId.HasValue ? Configuration["Files:PublicEndpoint"] + s.Person.PhotoId : string.Empty));
        }

        private void FilmPersonMappings()
        {
            CreateMap<PersonMarkDTO, PersonMark>();

            CreateMap<FilmPerson, FilmPersonPageDTO>()
                .ForMember(d => d.Roles, o => o.MapFrom(s => s.Roles.Where(x => x.Role != RoleType.Actor)))
                .ForMember(d => d.FilmMakings, o => o.MapFrom(s => s.Roles.Where(x => x.Role == RoleType.Actor)))
                .ForMember(d => d.Profession, o => o.MapFrom(s => s.Profession.ToString()))
                .ForMember(d => d.Rank, o => o.MapFrom(s => s.Marks.Select(x => x.Mark).Average()))
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.PhotoId.HasValue ? Configuration["Files:PublicEndpoint"] + s.PhotoId : string.Empty));

            CreateMap<PersonInRole, RoleInFilmDTO>()
                .ForMember(d => d.FilmName, o => o.MapFrom(s => s.Film.Title))
                .ForMember(d => d.YearOfProduction, o => o.MapFrom(s => s.Film.YearOfProduction))
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.Film.PhotoId.HasValue ? Configuration["Files:PublicEndpoint"] + s.Film.PhotoId : string.Empty));

            CreateMap<PersonInRole, FilmMakingDTO>()
                .ForMember(d => d.FilmName, o => o.MapFrom(s => s.Film.Title))
                .ForMember(d => d.Role, o => o.MapFrom(s => s.Role.ToString()))
                .ForMember(d => d.YearOfProduction, o => o.MapFrom(s => s.Film.YearOfProduction))
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.Film.PhotoId.HasValue ? Configuration["Files:PublicEndpoint"] + s.Film.PhotoId : string.Empty));
        }

        private void SearchMappings()
        {
            CreateMap<User, SearchDTO>()
                .ForMember(d => d.Value, o => o.MapFrom(s => s.FirstName + " " + s.Surname))
                .ForMember(d => d.Key, o => o.MapFrom(s => s.PublicId))
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.PhotoId.HasValue ? Configuration["Files:PublicEndpoint"] + s.PhotoId : string.Empty))
                .ForMember(d => d.Type, o => o.MapFrom(s => ItemType.User));

            CreateMap<Film, SearchDTO>()
                .ForMember(d => d.Value, o => o.MapFrom(s => s.Title))
                .ForMember(d => d.Key, o => o.MapFrom(s => s.PublicId))
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.PhotoId.HasValue ? Configuration["Files:PublicEndpoint"] + s.PhotoId : string.Empty))
                .ForMember(d => d.Type, o => o.MapFrom(s => ItemType.Film));

            CreateMap<FilmPerson, SearchDTO>()
                .ForMember(d => d.Value, o => o.MapFrom(s => s.FirstName + " " + s.Surname))
                .ForMember(d => d.Key, o => o.MapFrom(s => s.PublicId))
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.PhotoId.HasValue ? Configuration["Files:PublicEndpoint"] + s.PhotoId : string.Empty))
                .ForMember(d => d.Type, o => o.MapFrom(s => ItemType.FilmPerson));
        }

        private void FilesMappings()
        {
            CreateMap<StoredFileInfo, FileDTO>()
                .ForMember(d => d.ContentType, o => o.MapFrom(s => s.MimeType))
                .ForMember(d => d.FileName, o => o.MapFrom(s => s.DisplayName));
        }

        private void SharedMappings()
        {
            CreateMap<ForumThread, ForumElementDTO>()
                .ForMember(d => d.CreatedByName, o => o.MapFrom(s => s.CreatedBy.FirstName + " " + s.CreatedBy.Surname))
                .ForMember(d => d.Title, o => o.MapFrom(s => s.Subject))
                .ForMember(d => d.Text, o => o.MapFrom(s => s.Content))
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.CreatedBy.PhotoId.HasValue ? Configuration["Files:PublicEndpoint"] + s.CreatedBy.PhotoId : string.Empty));
        }
    }
}
