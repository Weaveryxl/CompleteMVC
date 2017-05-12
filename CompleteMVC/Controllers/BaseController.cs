using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompleteMVC.Infrastructure;

namespace CompleteMVC.Controllers
{
    public class BaseController : Controller
    {
        // GET: BaseController
        protected virtual new SchoolPrincipal User
        {
            get { return HttpContext.User as SchoolPrincipal; }
        }
        
    }
}