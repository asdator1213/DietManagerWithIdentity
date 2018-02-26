using Postal;

namespace DietManagerIdentity.ViewModels
{
    public class AccountCreatedViewModel:Email
    {
        public string To { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
    }
}