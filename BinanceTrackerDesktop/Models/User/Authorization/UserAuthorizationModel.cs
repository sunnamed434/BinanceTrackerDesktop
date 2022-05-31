namespace BinanceTrackerDesktop.Models.User.Authorization;

public sealed class UserAuthorizationModel
{
    public string Key;

    public string Secret;

    public string Currency;



    public UserAuthorizationModel(string key, string secret, string currency)
    {
        Key = key;
        Secret = secret;
        Currency = currency;
    }
}
