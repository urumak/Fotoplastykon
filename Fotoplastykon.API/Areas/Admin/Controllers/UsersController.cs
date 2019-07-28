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
        [Route("")]
        public async Task<IEnumerable<CoreUser>> GetAll()
        {
            using (var unitOfWork = new UnitOfWork(new DatabaseContext()))
            {
                return unitOfWork.CoreUsers.GetAll();
            }
        }
    }
}