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

namespace Fotoplastykon.API.AccessHandlers.PageAccess
{
    public class PageAccessHandler : AuthorizationHandler<PageAccessRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PageAccessRequirement requirement)
        {
            //var pagesClaim = context.User.FindFirst("CanEditPages");

            //if (pagesClaim == null)
            //{
            //    return;
            //}

            //var pagesIds = JsonConvert.DeserializeObject<List<string>>(pagesClaim.Value);

            //if (context.Resource is AuthorizationFilterContext authContext)
            //{
            //    if(authContext.RouteData.Values["id"] != null)
            //    {
            //        var pageId = authContext.RouteData.Values["id"].ToString();

            //        if (pagesIds.Contains(pageId))
            //        {
            //            context.Succeed(requirement);
            //        }
            //        else
            //        {
            //            context.Fail();
            //        }
            //    }
            //}
        }
    }
}
