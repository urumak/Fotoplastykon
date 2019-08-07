using AutoMapper;
using Fotoplastykon.API.Areas.Admin.Models.Users;
using Fotoplastykon.LL.Models.Users;

namespace Fotoplastykon.API.Areas.Admin.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            AddUserMappings();
        }

        private void AddUserMappings()
        {
            CreateMap<UserFormModel, AddUserModel>();
        }
    }
}
