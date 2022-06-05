using BinanceTrackerDesktop.Localizations.Language;

namespace BinanceTrackerDesktop.Models.User.Authorization;

public sealed class UserAuthorizationModel
{
    public string Key;

    public string Secret;

    public string Currency;

    public Languages Language;



    public UserAuthorizationModel(string key, string secret, string currency, Languages language)
    {
        Key = key;
        Secret = secret;
        Currency = currency;
        Language = language;
    }
}
