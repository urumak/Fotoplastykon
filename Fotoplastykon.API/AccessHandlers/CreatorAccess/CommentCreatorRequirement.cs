using Fotoplastykon.BLL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fotoplastykon.API.AccessHandlers.CreatorAccess
{
    public class CommentCreatorRequirement : ICreatorAccessRequirement
    {
        protected IInformationsService Information { get; set; }
        public CommentCreatorRequirement(IInformationsService information)
        {
            Information = information;
        }
        public async Task<long?> GetCreatedById(long itemId)
        {
            return (await Information.GetComment(itemId))?.CreatedById;
        }
    }
}
