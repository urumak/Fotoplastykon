using System.Linq;
using AutoMapper;
using Fotoplastykon.BLL.Models.FilmPeople;
using Fotoplastykon.BLL.Models.Films;
using Fotoplastykon.BLL.Models.Quizes;
using Fotoplastykon.BLL.Models.Users;
using Fotoplastykon.DAL.Entities.Concrete;
using User = Fotoplastykon.DAL.Entities.Concrete.User;

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
    }
}
