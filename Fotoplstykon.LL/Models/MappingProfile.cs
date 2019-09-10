using System.Linq;
using AutoMapper;
using Fotoplastykon.BLL.Models.Users;
using User = Fotoplastykon.DAL.Entities.Concrete.User;

namespace Fotoplastykon.BLL.Models
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
        }
    }
}
