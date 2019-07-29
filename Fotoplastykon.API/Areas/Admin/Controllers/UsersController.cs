using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fotoplastykon.DAL;
using Fotoplastykon.DAL.Entities.Core;
using Fotoplastykon.DAL.Repositories;
using Fotoplastykon.DAL.UnitsOfWork;
using Fotoplstykon.LL.Models;
using Fotoplstykon.LL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fotoplastykon.API.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        protected IUserService Users { get; }
        public UsersController(IUserService users)
        {
            Users = users;
        }
        [Route("")]
        public async Task<IEnumerable<User>> GetAll()
        {
            return Users.GetAll();
        }
    }
}