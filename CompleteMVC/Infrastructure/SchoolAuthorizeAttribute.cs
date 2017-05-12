using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CompleteMVC.Infrastructure
{
    public class SchoolAuthorizeAttribute : AuthorizeAttribute
    {
        protected virtual SchoolPrincipal CurrentUser
        {
            get { return HttpContext.Current.User as SchoolPrincipal; }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                Roles = string.Join(",", CurrentUser.Roles);
            }
            if (!string.IsNullOrEmpty(Roles))
            {
                if (!CurrentUser.IsInRole(Roles))
                {
                    filterContext.Result =
                        new RedirectToRouteResult(
                            new RouteValueDictionary(new {controller = "Error", action = "AccessDenied"}));
                }
            }
            base.OnAuthorization(filterContext);
        }
    }
}