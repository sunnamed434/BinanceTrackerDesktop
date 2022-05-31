using BinanceTrackerDesktop.Models.User.Authentication.Validation;
using BinanceTrackerDesktop.MVC.Controller;
using BinanceTrackerDesktop.User.Authentication.System;
using BinanceTrackerDesktop.User.Authentication.System.Result;
using BinanceTrackerDesktop.Views.Authenticator;

namespace BinanceTrackerDesktop.Controllers;

public sealed class AuthenticatorController : Controller<AuthenticatorController>
{
    private readonly UserAuthenticatorSystem authenticatorSystem;



    public AuthenticatorController(IAuthenticatorView view) : base(view)
    {
        authenticatorSystem = new UserAuthenticatorSystem();
    }



    public ValidateResult Validate(UserValidationModel validation)
    {
        if (validation == null)
        {
            throw new ArgumentNullException(nameof(validation));
        }

        return authenticatorSystem.ValidateTwoFactor(validation.Secret, validation.PIN);
    }



    protected override AuthenticatorController InitializeController()
    {
        return this;
    }
}
