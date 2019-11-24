using Fotoplastykon.BLL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fotoplastykon.API.AccessHandlers.CreatorAccess
{
    public class ForumThreadCommentRequirement : ICreatorAccessRequirement
    {
        protected IForumService Forum { get; set; }
        public ForumThreadCommentRequirement(IForumService forum)
        {
            Forum = forum;
        }
        public async Task<long?> GetCreatedById(long itemId)
        {
            return (await Forum.GetComment(itemId))?.CreatedById;
        }
    }
}
