using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DietManagerIdentity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace DietManagerIdentity.Helpers
{
    public static class UserHelper
    {
        public static ApplicationUser GetLoggedUser()
        {
            return HttpContext.Current.GetOwinContext()
                .GetUserManager<ApplicationUserManager>()
                .FindById(HttpContext.Current.User.Identity.GetUserId());
        }
    }
}