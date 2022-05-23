using BinanceTrackerDesktop.Core.Models.Authentication.Validation;
using BinanceTrackerDesktop.Core.User.Authentication.System;
using BinanceTrackerDesktop.Core.User.Authentication.System.Result;
using BinanceTrackerDesktop.Core.Views.Authenticator;

namespace BinanceTrackerDesktop.Core.Controllers
{
    public sealed class AuthenticatorController
    {
        private readonly IAuthenticatorView view;

        private readonly UserAuthenticatorSystem authenticatorSystem;



        public AuthenticatorController(IAuthenticatorView view)
        {
            this.view = view ?? throw new ArgumentNullException(nameof(view));
            this.view.SetController(this);
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
    }
}
