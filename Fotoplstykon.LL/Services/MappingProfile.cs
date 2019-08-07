using System.Linq;
using AutoMapper;
using Fotoplastykon.LL.Models.Users;
using User = Fotoplastykon.DAL.Entities.Concrete.User;

namespace Fotoplastykon.LL.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            AddUserMappings();
        }

        private void AddUserMappings()
        {
            CreateMap<User, AddUserModel>()
                .ReverseMap()
                .ForMember(d => d.PasswordHash, o => o.Ignore());

            CreateMap<User, UserLoginModel>()
                .ForMember(d => d.PagesIds, o => o.MapFrom(s => s.PageCreations.Select(c => c.FilmId)));
        }
    }
}
