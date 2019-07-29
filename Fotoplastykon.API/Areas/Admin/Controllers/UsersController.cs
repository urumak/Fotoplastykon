using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fotoplastykon.DAL;
using Fotoplastykon.DAL.Entities.Core;
using Fotoplastykon.DAL.UnitsOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fotoplastykon.API.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        protected DatabaseContext Context { get; }
        public UsersController(DatabaseContext context)
        {
            Context = context;
        }
        [Route("")]
        public async Task<IEnumerable<CoreUser>> GetAll()
        {
            using (var unitOfWork = new UnitOfWork(Context))
            {
                return unitOfWork.CoreUsers.GetAll();
            }
        }
    }
}