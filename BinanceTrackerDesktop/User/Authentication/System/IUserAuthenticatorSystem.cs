using BinanceTrackerDesktop.User.Authentication.System.Result;

namespace BinanceTrackerDesktop.User.Authentication.System;

public interface IUserAuthenticatorSystem
{
    Image Authenticate(string title, string secret);

    ValidateResult ValidateTwoFactor(string secret, string pin);
}
