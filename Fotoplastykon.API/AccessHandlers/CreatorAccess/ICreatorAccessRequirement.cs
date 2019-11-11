using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fotoplastykon.API.AccessHandlers.CreatorAccess
{
    public interface ICreatorAccessRequirement : IAuthorizationRequirement
    {
        Task<long?> GetIdsOfCreatedEntities(long userId);
    }
}
