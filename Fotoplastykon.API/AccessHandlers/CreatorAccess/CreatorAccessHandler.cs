using Fotoplastykon.DAL.Entities.Abstract;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Fotoplastykon.API.AccessHandlers.CreatorAccess
{
    public class CreatorAccessHandler : AuthorizationHandler<ICreatorAccessRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ICreatorAccessRequirement requirement)
        {
            var userId = context.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;

            if (string.IsNullOrEmpty(userId)) return;

            if (context.Resource is AuthorizationFilterContext authContext)
            {
                if (authContext.RouteData.Values["id"] != null)
                {
                    var itemId = Convert.ToInt64(authContext.RouteData.Values["id"]);
                    var itemCreatorId = await requirement.GetIdsOfCreatedEntities(itemId);

                    if (itemCreatorId == null) context.Succeed(requirement);
                    if (itemCreatorId == Convert.ToInt64(userId)) context.Succeed(requirement);
                    else context.Fail();
                }
            }
        }
    }
}
