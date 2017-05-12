using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace CompleteMVC.Infrastructure
{
    public class SchoolPrincipal : IPrincipal
    {
        public bool IsInRole(string role)
        {
            return Roles.Any(r => r.Contains(role));
        }

        public SchoolPrincipal(string UserName)
        {
            this.Identity = new GenericIdentity(UserName);
        }

        public IIdentity Identity { get; private set; }

        public string UserName { get; private set; }
        public int UserId { get; set; }
        public string[] Roles { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}