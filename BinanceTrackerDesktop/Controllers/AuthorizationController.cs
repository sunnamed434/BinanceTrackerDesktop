using BinanceTrackerDesktop.Forms.Authorization;
using BinanceTrackerDesktop.Models.User.Authorization;
using BinanceTrackerDesktop.MVC.Controller;
using BinanceTrackerDesktop.User.Data;
using BinanceTrackerDesktop.User.Data.Builder;
using BinanceTrackerDesktop.User.Data.Extension;
using BinanceTrackerDesktop.User.Data.Save.Binary;
using BinanceTrackerDesktop.Validators.String.Extension;
using BinanceTrackerDesktop.Views.Authorization;
using BinanceTrackerDesktop.Views.Authorization.Exceptions;
using BinanceTrackerDesktop.Views.Authorization.Exceptions.ErrorCode;

namespace BinanceTrackerDesktop.Controllers;

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
                    .AddUserLanguage(authorizationModel.Language)
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
                .AddUserLanguage(authorizationModel.Language)
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
