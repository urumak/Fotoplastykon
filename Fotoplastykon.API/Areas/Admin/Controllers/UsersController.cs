using System.Collections.Generic;
using AutoMapper;
using Fotoplastykon.API.Areas.Admin.Models.Users;
using Fotoplastykon.BLL.Models.Users;
using Fotoplastykon.BLL.Services.Abstract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fotoplastykon.API.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminAccess")]
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
        public IEnumerable<AddUserModel> Search(string search)
        {
            return null;
        }
    }
}