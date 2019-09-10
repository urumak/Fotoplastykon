using AutoMapper;
using Fotoplastykon.API.Areas.Admin.Models.Users;
using Fotoplastykon.BLL.Models.Users;

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
