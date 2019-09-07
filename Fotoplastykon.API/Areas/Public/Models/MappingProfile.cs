using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fotoplastykon.API.Areas.Public.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            AddAuthMappings();
        }

        void AddAuthMappings()
        {

        }
    }
}
