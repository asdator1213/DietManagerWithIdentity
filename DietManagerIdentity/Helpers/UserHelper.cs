using System.Web;
using Microsoft.AspNet.Identity;
using DietManagerIdentity.Models;
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