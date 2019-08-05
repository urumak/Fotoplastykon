using AutoMapper;
using Fotoplastykon.LL.Models;
using User = Fotoplastykon.DAL.Entities.Concrete.User;

namespace Fotoplastykon.LL.Services
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<User, Models.User>();
        }
    }
}
