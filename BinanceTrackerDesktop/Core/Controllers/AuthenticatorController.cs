using BinanceTrackerDesktop.Core.Models.Authentication.Validation;
using BinanceTrackerDesktop.Core.MVC.Controller;
using BinanceTrackerDesktop.Core.User.Authentication.System;
using BinanceTrackerDesktop.Core.User.Authentication.System.Result;
using BinanceTrackerDesktop.Core.Views.Authenticator;

namespace BinanceTrackerDesktop.Core.Controllers
{
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
}
