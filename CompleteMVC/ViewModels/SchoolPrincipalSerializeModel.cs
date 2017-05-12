using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompleteMVC.ViewModels
{
    public class SchoolPrincipalSerializeModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string[] Roles { get; set; }

    }
}