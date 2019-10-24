using Fotoplastykon.BLL.DTOs.Search;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public interface ISearchService
    {
        Task<List<SearchDTO>> Search(string search, int limit = 10);
    }
}
