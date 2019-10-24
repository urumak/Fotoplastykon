﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fotoplastykon.BLL.Models.Search;
using Fotoplastykon.BLL.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fotoplastykon.API.Areas.Public.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        protected ISearchService SearchService { get; }
        private IMapper Mapper { get; }

        public SearchController(ISearchService searchService, IMapper mapper)
        {
            Mapper = mapper;
            SearchService = searchService;
        }

        [HttpGet("{search}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Search(string search)
        {
            return Ok(await SearchService.Search(search));
        }
    }
}