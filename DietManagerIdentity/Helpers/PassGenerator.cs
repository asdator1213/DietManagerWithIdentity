using System.Linq;
using System.Web.Security;

namespace DietManagerIdentity.Helpers
{
    public static class PassGenerator
    {
        public static string Generate()
        {
            string password = Membership.GeneratePassword(16, 2);
            while (!password.Any(c => char.IsDigit(c))
                   && (!password.Contains("0") || !password.Contains("o") ||
                       !password.Contains("l") || !password.Contains("I")))
            {
                password = Membership.GeneratePassword(16, 2);
            }

            return password;
        }
    }
}