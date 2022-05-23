using BinanceTrackerDesktop.Core.Forms.Authorization;
using BinanceTrackerDesktop.Core.Models.User.Authorization;
using BinanceTrackerDesktop.Core.MVC.Controller;
using BinanceTrackerDesktop.Core.User.Data;
using BinanceTrackerDesktop.Core.User.Data.Builder;
using BinanceTrackerDesktop.Core.User.Data.Extension;
using BinanceTrackerDesktop.Core.User.Data.Save.Binary;
using BinanceTrackerDesktop.Core.Validators.String.Extension;
using BinanceTrackerDesktop.Core.Views.Authorization;
using BinanceTrackerDesktop.Core.Views.Authorization.Exceptions;
using BinanceTrackerDesktop.Core.Views.Authorization.Exceptions.ErrorCode;

namespace BinanceTrackerDesktop.Core.Controllers
{
    public sealed class AuthorizationController : Controller<AuthorizationController>
    {
        public AuthorizationController(IAuthorizationView view) : base(view)
        {
        }



        public void Authorize(UserAuthorizationModel authorizationModel)
        {
            if (authorizationModel == null)
            {
                throw new ArgumentNullException(nameof(authorizationModel));
            }

            authorizationModel.Key.Rules()
               .ContentNotNullOrWhiteSpace()
               .MinCharacters(BinanceAPIKeysCharactersLength.MaxLengthSecretKey)
               .ThrowIfFailed(new AuthorizationException(nameof(authorizationModel.Key), AuthorizationErrorCode.Key));

            authorizationModel.Secret.Rules()
               .ContentNotNullOrWhiteSpace()
               .MinCharacters(BinanceAPIKeysCharactersLength.MaxLengthSecretKey)
               .ThrowIfFailed(new AuthorizationException(nameof(authorizationModel.Secret), AuthorizationErrorCode.Secret));

            authorizationModel.Currency.Rules()
                .ContentNotNullOrWhiteSpace()
                .ThrowIfFailed(new AuthorizationException(nameof(authorizationModel.Currency), AuthorizationErrorCode.Currency));

            UserData userData = new BinaryUserDataSaveSystem().Read();
            if (userData != null)
            {
                if (userData.HasAuthenticationData)
                {
                    new UserDataBuilder(userData)
                        .AddKey(authorizationModel.Key)
                        .AddSecret(authorizationModel.Secret)
                        .AddCurrency(authorizationModel.Currency)
                        .Build()
                        .WriteUserData(new BinaryUserDataSaveSystem());
                }
            }
            else
            {
                new UserDataBuilder()
                    .AddKey(authorizationModel.Key)
                    .AddSecret(authorizationModel.Secret)
                    .AddCurrency(authorizationModel.Currency)
                    .SetNotificationsEnabled()
                    .SetAsUserStartedApplicationFirstTime()
                    .SetUserThemeAsSystem()
                    .Build()
                    .WriteUserData(new BinaryUserDataSaveSystem());
            }
        }



        protected override AuthorizationController InitializeController()
        {
            return this;
        }
    }
}
