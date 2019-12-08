﻿using System.Linq;
using AutoMapper;
using Fotoplastykon.BLL.Enums;
using Fotoplastykon.BLL.DTOs.FilmPeople;
using Fotoplastykon.BLL.DTOs.Films;
using Fotoplastykon.BLL.DTOs.Quizzes;
using Fotoplastykon.BLL.DTOs.Search;
using Fotoplastykon.BLL.DTOs.Users;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.BLL.DTOs.Files;
using Microsoft.Extensions.Configuration;
using Fotoplastykon.BLL.DTOs.Shared;
using Fotoplastykon.DAL.Enums;
using Fotoplastykon.BLL.DTOs.Chat;
using Fotoplastykon.BLL.DTOs.SignalR;

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
            InformationMappings();
            ForumMappings();
            QuizzesMappings();
            ChatMappings();
            SignalRMappings();
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
                .ForMember(d => d.Rating, o => o.MapFrom(s => s.Watchings.Select(x => x.Mark).Average()))
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.PhotoId.HasValue ? Configuration["Files:PublicEndpoint"] + s.PhotoId : string.Empty));

            CreateMap<PersonInRole, CastMemberDTO>()
                .ForMember(d => d.FullName, o => o.MapFrom(s => s.Person.FirstName + " " + s.Person.Surname))
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.Person.PhotoId.HasValue ? Configuration["Files:PublicEndpoint"] + s.Person.PhotoId : string.Empty));

            CreateMap<PersonInRole, FilmmakerDTO>()
                .ForMember(d => d.FullName, o => o.MapFrom(s => s.Person.FirstName + " " + s.Person.Surname))
                .ForMember(d => d.Profession, o => o.MapFrom(s => s.Person.Profession.ToString()))
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.Person.PhotoId.HasValue ? Configuration["Files:PublicEndpoint"] + s.Person.PhotoId : string.Empty));
        }

        private void FilmPersonMappings()
        {
            CreateMap<PersonMarkDTO, PersonMark>();

            CreateMap<FilmPerson, FilmPersonPageDTO>()
                .ForMember(d => d.Roles, o => o.MapFrom(s => s.Roles))
                .ForMember(d => d.Profession, o => o.MapFrom(s => s.Profession.ToString()))
                .ForMember(d => d.Rating, o => o.MapFrom(s => s.Marks.Select(x => x.Mark).Average()))
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.PhotoId.HasValue ? Configuration["Files:PublicEndpoint"] + s.PhotoId : string.Empty));

            CreateMap<PersonInRole, RoleInFilmDTO>()
                .ForMember(d => d.FilmName, o => o.MapFrom(s => s.Film.Title))
                .ForMember(d => d.YearOfProduction, o => o.MapFrom(s => s.Film.YearOfProduction))
                .ForMember(d => d.RoleDescription, o => o.MapFrom(s => s.Role == RoleType.Actor ? s.CharacterName : s.Role.ToString()))
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.Film.PhotoId.HasValue ? Configuration["Files:PublicEndpoint"] + s.Film.PhotoId : string.Empty));
        }

        private void SearchMappings()
        {
            CreateMap<User, SearchDTO>()
                .ForMember(d => d.Value, o => o.MapFrom(s => s.FirstName + " " + s.Surname))
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.PhotoId.HasValue ? Configuration["Files:PublicEndpoint"] + s.PhotoId : string.Empty))
                .ForMember(d => d.Type, o => o.MapFrom(s => ItemType.User));

            CreateMap<Film, SearchDTO>()
                .ForMember(d => d.Value, o => o.MapFrom(s => s.Title))
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.PhotoId.HasValue ? Configuration["Files:PublicEndpoint"] + s.PhotoId : string.Empty))
                .ForMember(d => d.Type, o => o.MapFrom(s => ItemType.Film));

            CreateMap<FilmPerson, SearchDTO>()
                .ForMember(d => d.Value, o => o.MapFrom(s => s.FirstName + " " + s.Surname))
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
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.CreatedBy.PhotoId.HasValue ? Configuration["Files:PublicEndpoint"] + s.CreatedBy.PhotoId : string.Empty));
        }

        private void InformationMappings()
        {
            CreateMap<Information, DTOs.Information.ListItem>()
               .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.PhotoId.HasValue ? Configuration["Files:PublicEndpoint"] + s.PhotoId : string.Empty));

            CreateMap<Information, DTOs.Information.InformationDTO>()
                .ForMember(d => d.CreatedByName, o => o.MapFrom(s => s.CreatedBy.FirstName + " " + s.CreatedBy.Surname))
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.PhotoId.HasValue ? Configuration["Files:PublicEndpoint"] + s.PhotoId : string.Empty));

            CreateMap<InformationComment, DTOs.Information.CommentDTO>()
                .ForMember(d => d.CreatorFullName, o => o.MapFrom(s => s.CreatedBy.FirstName + " " + s.CreatedBy.Surname))
                .ForMember(d => d.Replies, o => o.MapFrom(s => s.Replies.OrderBy(r => r.DateCreated)))
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.CreatedBy.PhotoId.HasValue ? Configuration["Files:PublicEndpoint"] + s.CreatedBy.PhotoId : string.Empty))
                .ForMember(d => d.IsDeleted, o => o.MapFrom(s => s.DateDeleted.HasValue))
                .AfterMap((s, d) =>
                {
                    if (s.DateDeleted.HasValue) d.Content = Configuration["DeletedItems:ItemDescription"];
                    if (s.CreatedBy.AnonimisationDate.HasValue) d.CreatorFullName = Configuration["DeletedItems:UserDescription"];
                });

            CreateMap<DTOs.Information.CommentDTO, InformationComment>()
                .ForMember(d => d.CreatedById, o => o.Ignore())
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.DateCreated, o => o.Ignore())
                .ForMember(d => d.DateDeleted, o => o.Ignore())
                .ForMember(d => d.Replies, o => o.Ignore());
        }

        private void ForumMappings()
        {
            CreateMap<ForumThread, DTOs.Forum.ForumListItemDTO>()
                .ForMember(d => d.CreatedByName, o => o.MapFrom(s => s.CreatedBy.FirstName + " " + s.CreatedBy.Surname))
                .ForMember(d => d.IsDeleted, o => o.MapFrom(s => s.DateDeleted.HasValue))
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.CreatedBy.PhotoId.HasValue ? Configuration["Files:PublicEndpoint"] + s.CreatedBy.PhotoId : string.Empty)).AfterMap((s, d) =>
                {
                    if (s.CreatedBy.AnonimisationDate.HasValue) d.CreatedByName = Configuration["DeletedItems:UserDescription"];
                    if (s.DateDeleted.HasValue) d.Content = Configuration["DeletedItems:ItemDescription"];
                });

            CreateMap<ForumThread, DTOs.Forum.ForumThreadDTO>()
                .ForMember(d => d.CreatedByName, o => o.MapFrom(s => s.CreatedBy.FirstName + " " + s.CreatedBy.Surname))
                .ForMember(d => d.IsDeleted, o => o.MapFrom(s => s.DateDeleted.HasValue))
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.CreatedBy.PhotoId.HasValue ? Configuration["Files:PublicEndpoint"] + s.CreatedBy.PhotoId : string.Empty))
                .AfterMap((s, d) =>
                {
                    if (s.CreatedBy.AnonimisationDate.HasValue) d.CreatedByName = Configuration["DeletedItems:UserDescription"];
                    if (s.DateDeleted.HasValue) d.Content = Configuration["DeletedItems:ItemDescription"];
                })
                .ReverseMap()
                .ForMember(d => d.CreatedById, o => o.Ignore())
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.DateCreated, o => o.Ignore());

            CreateMap<ForumThreadComment, DTOs.Forum.ForumThreadCommentDTO>()
                .ForMember(d => d.CreatedByName, o => o.MapFrom(s => s.CreatedBy.FirstName + " " + s.CreatedBy.Surname))
                .ForMember(d => d.IsDeleted, o => o.MapFrom(s => s.DateDeleted.HasValue))
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.CreatedBy.PhotoId.HasValue ? Configuration["Files:PublicEndpoint"] + s.CreatedBy.PhotoId : string.Empty))
                .AfterMap((s, d) =>
                {
                    if (s.CreatedBy.AnonimisationDate.HasValue) d.CreatedByName = Configuration["DeletedItems:UserDescription"];
                    if (s.DateDeleted.HasValue) d.Content = Configuration["DeletedItems:ItemDescription"];
                })
                .ReverseMap()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Replies, o => o.Ignore());
        }

        private void ChatMappings()
        {
            CreateMap<User, ChatListItemDTO>()
                .ForMember(d => d.NameAndSurname, o => o.MapFrom(s => s.FirstName + " " + s.Surname))
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.PhotoId.HasValue ? Configuration["Files:PublicEndpoint"] + s.PhotoId : string.Empty));
        }

        private void QuizzesMappings()
        {
            CreateMap<Quiz, QuizModel>();
            CreateMap<Quiz, ListItemModel>();
            CreateMap<QuizQuestion, QuestionModel>();
            CreateMap<QuizAnswer, AnswerModel>();
            CreateMap<QuestionModel, QuestionResultDTO>();
            CreateMap<AnswerModel, AnswerResultDTO>()
                .ForMember(d => d.IsCorrect, o => o.Ignore());
        }

        private void SignalRMappings()
        {
            CreateMap<SignalRConnectionDTO, SignalRConnection>()
                .ForMember(d => d.DateDeleted, o => o.Ignore())
                .ForMember(d => d.DateCreated, o => o.Ignore())
                .ForMember(d => d.User, o => o.Ignore())
                .ForMember(d => d.Id, o => o.Ignore())
                .ReverseMap();
        }
    }
}
