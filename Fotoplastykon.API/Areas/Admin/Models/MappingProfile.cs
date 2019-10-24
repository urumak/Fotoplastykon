using AutoMapper;
using Fotoplastykon.API.Areas.Admin.Models.Users;
using Fotoplastykon.BLL.DTOs.Users;
using Fotoplastykon.DAL.Entities.Concrete;

namespace Fotoplastykon.API.Areas.Admin.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            UsersMappings();
        }

        private void UsersMappings()
        {
            CreateMap<UserFormModel, AddUserDTO>();
        }
    }
}
