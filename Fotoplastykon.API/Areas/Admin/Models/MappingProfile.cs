using AutoMapper;
using Fotoplastykon.API.Areas.Admin.Models.Users;
using Fotoplastykon.BLL.DTOs.Users;
using Fotoplastykon.DAL.Entities.Concrete;
using Microsoft.Extensions.Configuration;

namespace Fotoplastykon.API.Areas.Admin.Models
{
    public class MappingProfile : Profile
    {
        private IConfiguration Configuration { get; }
        public MappingProfile(IConfiguration configuration)
        {
            Configuration = configuration;
            UsersMappings();
        }

        private void UsersMappings()
        {
            CreateMap<User, UserFormModel>()
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.PhotoId.HasValue ? Configuration["Files:PublicEndpoint"] + s.PhotoId : string.Empty))
                .ForMember(d => d.Password, o => o.Ignore())
                .ForMember(d => d.RepeatPassword, o => o.Ignore())
                .ReverseMap();

            CreateMap<UserFormModel, AddUserDTO>();
        }
    }
}
