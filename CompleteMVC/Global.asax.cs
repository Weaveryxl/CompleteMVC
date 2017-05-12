using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using CompleteMVC.Infrastructure;
using CompleteMVC.ViewModels;
using Newtonsoft.Json;

namespace CompleteMVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value); // why do authCookie has the data we need? where did it get them???

                if (authTicket != null) //here we could also checkif it's expired
                {
                    //The following part is different with authentication2
                    //Need to figure out why
                    SchoolPrincipalSerializeModel serializeModel =
                        JsonConvert.DeserializeObject<SchoolPrincipalSerializeModel>(authTicket.UserData); // turned from JSON back to original form
                    var newUser = new SchoolPrincipal(authTicket.Name)  //name the new principal with the username
                    {                                                   // generally, as long as user has Iprincipal, things would be find
                        UserId = serializeModel.UserId,
                        FirstName = serializeModel.FirstName,
                        LastName = serializeModel.LastName,
                        Roles = serializeModel.Roles
                    };

                    //if (newUser != null)
                    //{
                        HttpContext.Current.User = newUser;
                    //}
                    
                }
            }
        }
    }
}