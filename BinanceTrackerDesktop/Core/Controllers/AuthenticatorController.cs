using BinanceTrackerDesktop.Core.Models.Authentication.Validation;
using BinanceTrackerDesktop.Core.User.Authentication.System;
using BinanceTrackerDesktop.Core.User.Authentication.System.Result;

namespace BinanceTrackerDesktop.Core.Controllers
{
    public sealed class AuthenticatorController
    {
        private readonly UserAuthenticatorSystem authenticatorSystem;



        public AuthenticatorController()
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
    }
}
