using System.Collections.Generic;
using Fotoplstykon.LL.Models;
using Fotoplstykon.LL.Services;
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
        public IEnumerable<User> GetAll()
        {
            return Users.GetAll();
        }
    }
}