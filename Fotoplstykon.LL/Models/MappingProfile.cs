using System.Linq;
using AutoMapper;
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
    }
}
