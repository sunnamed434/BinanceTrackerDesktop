using BinanceTrackerDesktop.Models.User.Authentication;
using BinanceTrackerDesktop.MVC.Controller;
using BinanceTrackerDesktop.User.Authentication.System;
using BinanceTrackerDesktop.Views.Authentication;

namespace BinanceTrackerDesktop.Controllers;

public sealed class AuthenticationController : Controller<AuthenticationController>
{
    private readonly UserAuthenticatorSystem authenticatorSystem;



    public AuthenticationController(IAuthenticationView view) : base(view)
    {
        authenticatorSystem = new UserAuthenticatorSystem();
    }



    public Image AuthenticateAndGenerateQRCodeImage(UserAuthenticationModel userAuthentication)
    {
        if (userAuthentication == null)
        {
            throw new ArgumentNullException(nameof(userAuthentication));
        }

        return authenticatorSystem.Authenticate(userAuthentication.AccountTitle, userAuthentication.SecretKey);
    }



    protected override AuthenticationController InitializeController()
    {
        return this;
    }
}
