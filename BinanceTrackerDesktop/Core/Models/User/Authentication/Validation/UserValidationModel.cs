namespace BinanceTrackerDesktop.Core.Models.Authentication.Validation
{
    public sealed class UserValidationModel
    {
        public string Secret;

        public string PIN;



        public UserValidationModel(string secret, string pin)
        {
            Secret = secret;
            PIN = pin;
        }
    }
}
