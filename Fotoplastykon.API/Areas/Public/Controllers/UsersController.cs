using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fotoplastykon.API.Areas.Public.Models;
using Fotoplastykon.API.Areas.Public.Models.Users;
using Fotoplastykon.API.Enums;
using Fotoplastykon.API.Extensions;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.DAL.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fotoplastykon.API.Areas.Public.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUsersService Users { get; }
        private IMapper Mapper { get; }

        public UsersController(IUsersService users, IMapper mapper)
        {
            Users = users;
            Mapper = mapper;
        }

        [HttpGet("{search}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IEnumerable<ListItemModel> Search(string search)
        {
            return Mapper.Map<List<ListItemModel>>(Users.Search(search));
        }
    }
}