using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fotoplastykon.API.Areas.Public.Models.Informations;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.Tools.Pager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fotoplastykon.API.Areas.Public.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformationsController : ControllerBase
    {
        private IInformationSerivice Informations { get; }
        private IMapper Mapper { get; }

        public InformationsController(IInformationSerivice informations, IMapper mapper)
        {
            Informations = informations;
            Mapper = mapper;
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public IActionResult GetPaginatedList([FromQuery]Pager pager)
        {
            var result = Informations.GetPaginatedList(pager);

            return Ok(new PaginationResult<ListItemModel>
            {
                Pager = result.Pager,
                Items = Mapper.Map<List<ListItemModel>>(result.Items)
            });
        }
    }
}