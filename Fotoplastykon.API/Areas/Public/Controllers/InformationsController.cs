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
    [Route("api/informations")]
    [ApiController]
    public class InformationsController : ControllerBase
    {
        private IInformationsService Informations { get; }
        private IMapper Mapper { get; }

        public InformationsController(IInformationsService informations, IMapper mapper)
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

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Get(long id)
        {
            var result = Informations.GetWithCreator(id);
            if (result == null) return NotFound();

            return Ok(Mapper.Map<InformationModel>(result));
        }
    }
}