using System.Collections.Generic;
using AutoMapper;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using Fotoplastykon.LL.Models;
using Fotoplastykon.LL.Services.Abstract;

namespace Fotoplastykon.LL.Services.Concrete
{
    public class UserService : IUserService
    {
        private IUnitOfWork Unit { get; }
        private IMapper Mapper { get; }
        public UserService (IUnitOfWork unit, IMapper mapper)
        {
            Unit = unit;
            Mapper = mapper;
        }

        public IEnumerable<User> GetAll()
        {
            return Mapper.Map<IEnumerable<User>>(Unit.CoreUsers.GetAll());
        }
    }
}
