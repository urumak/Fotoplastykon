using Fotoplastykon.BLL.Models.Search;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public interface ISearchService
    {
        Task<List<SearchModel>> Search(string search, int limit = 10);
    }
}
