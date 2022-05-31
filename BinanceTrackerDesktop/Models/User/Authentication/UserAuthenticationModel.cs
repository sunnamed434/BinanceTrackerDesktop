namespace BinanceTrackerDesktop.Models.User.Authentication;

public sealed class UserAuthenticationModel
{
    public string AccountTitle;

    public string SecretKey;



    public UserAuthenticationModel(string accountTitle, string secretKey)
    {
        AccountTitle = accountTitle;
        SecretKey = secretKey;
    }
}
