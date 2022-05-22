using BinanceTrackerDesktop.Core.Models.User.Authentication;
using BinanceTrackerDesktop.Core.User.Authentication.System;
using BinanceTrackerDesktop.Core.Views.Authentication;

namespace BinanceTrackerDesktop.Core.Controllers
{
    public sealed class AuthenticationController
    {
        private readonly IAuthenticationView view;

        private readonly UserAuthenticatorSystem authenticatorSystem;



        public AuthenticationController(IAuthenticationView view)
        {
            this.view = view ?? throw new ArgumentNullException(nameof(view));
            this.view.SetController(this);
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
    }
}
