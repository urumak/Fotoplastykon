using System.Collections.Generic;
using AutoMapper;
using Fotoplastykon.API.Areas.Admin.Models.Users;
using Fotoplastykon.BLL.DTOs.Users;
using Fotoplastykon.BLL.Services.Abstract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fotoplastykon.API.Areas.Admin.Controllers
{
    [Route("api/admin/users")]
    [ApiController]
    [Authorize(Policy = "AdminAccess")]
    public class UsersController : ControllerBase
    {
        protected IUsersService Users { get; }
        protected IMapper Mapper { get; }

        public UsersController(IUsersService users, IMapper mapper)
        {
            Users = users;
            Mapper = mapper;
        }
        //get list z filtrami i pagerem
        //create
        //fetch
        //update
        //delete
    }
}