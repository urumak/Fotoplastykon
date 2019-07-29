using Fotoplastykon.DAL.UnitsOfWork;
using Fotoplstykon.LL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;

namespace Fotoplstykon.LL.Services
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
