using AutoMapper;
using Fotoplastykon.DAL.Entities.Concrete.Core;
using Fotoplastykon.LL.Models;

namespace Fotoplastykon.LL.Services
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<CoreUser, User>();
        }
    }
}
