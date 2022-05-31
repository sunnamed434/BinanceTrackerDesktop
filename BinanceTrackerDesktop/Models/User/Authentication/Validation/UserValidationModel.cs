namespace BinanceTrackerDesktop.Models.User.Authentication.Validation;

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
