using System.Collections.Generic;
using AutoMapper;
using Fotoplastykon.API.Areas.Admin.Models.Users;
using Fotoplastykon.LL.Models.Users;
using Fotoplastykon.LL.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Fotoplastykon.API.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        protected IUserService Users { get; }
        protected IMapper Mapper { get; }

        public UsersController(IUserService users, IMapper mapper)
        {
            Users = users;
            Mapper = mapper;
        }
        [Route("")]
        public IEnumerable<AddUserModel> GetAll()
        {
            return Users.GetAll();
        }
    }
}