using BinanceTrackerDesktop.Core.Models.User.Authentication;
using BinanceTrackerDesktop.Core.MVC.Controller;
using BinanceTrackerDesktop.Core.User.Authentication.System;
using BinanceTrackerDesktop.Core.Views.Authentication;

namespace BinanceTrackerDesktop.Core.Controllers
{
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
}
