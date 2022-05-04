using BinanceTrackerDesktop.Core.User.Authentication.System.Result;

namespace BinanceTrackerDesktop.Core.User.Authentication.System
{
    public interface IUserAuthenticatorSystem
    {
        Image Authenticate(string title, string secret);

        ValidateResult ValidateTwoFactor(string secret, string pin);
    }
}
