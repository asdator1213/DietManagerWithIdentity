using DietManagerIdentity.ViewModels;

namespace DietManagerIdentity.Helpers
{
    public static class Mailing
    {
        public static void Send(AccountCreatedViewModel model)
        {
            model.Send();
        }
    }   
}