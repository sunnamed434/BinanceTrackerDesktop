using BinanceTrackerDesktop.Core.ApplicationInfo.Environment;
using BinanceTrackerDesktop.Core.Authentication.TwoFactor.Exception;
using BinanceTrackerDesktop.Core.Authentication.TwoFactor.Exception.ErrorCode;
using BinanceTrackerDesktop.Core.User.Authentication.System.Result;
using BinanceTrackerDesktop.Core.Validators.String.Extension;
using Google.Authenticator;

namespace BinanceTrackerDesktop.Core.User.Authentication.System
{
    public class UserAuthenticatorSystem : IUserAuthenticatorSystem
    {
        private const string IgnoringStringData = "data:image/png;base64,";

        private const int MinCharacters = 6;

        private const int MaxAccountTitleCharacters = 128;

        private const int MaxSecretCharacters = 128;

        private const int UnsupportedCharactersCount = 1;



        public Image Authenticate(string accountTitle, string secret)
        {
            accountTitle.Rules()
                .ContentNotNullOrEmpty()
                .MinCharacters(UnsupportedCharactersCount)
                .MaxCharacters(MaxAccountTitleCharacters)
                .ThrowIfFailed(new TwoFactorAuthenticationException(nameof(accountTitle), AuthenticationErrorCode.AccountTitle));

            secret.Rules()
                .ContentNotNullOrEmpty()
                .MinCharacters(UnsupportedCharactersCount)
                .MaxCharacters(MaxSecretCharacters)
                .ThrowIfFailed(new TwoFactorAuthenticationException(nameof(secret), AuthenticationErrorCode.Secret));

            SetupCode setupCode = new TwoFactorAuthenticator().GenerateSetupCode(ApplicationEnviroment.GlobalName, accountTitle.Trim(), secret, false);
            using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(setupCode.QrCodeSetupImageUrl.Replace(IgnoringStringData, string.Empty))))
                return Image.FromStream(memoryStream);
        }

        public ValidateResult ValidateTwoFactor(string secret, string pin)
        {
            secret.Rules()
                .ContentNotNullOrEmpty()
                .MinCharacters(MinCharacters)
                .ThrowIfFailed(new TwoFactorAuthenticationException(nameof(secret), AuthenticationErrorCode.Secret));

            pin.Rules()
                .ContentNotNullOrEmpty()
                .MinCharacters(MinCharacters)
                .ThrowIfFailed(new TwoFactorAuthenticationException(nameof(pin), AuthenticationErrorCode.PIN));

            return new TwoFactorAuthenticator().ValidateTwoFactorPIN(secret, pin) 
                ? ValidateResult.Successfully 
                : ValidateResult.Failed;
        }
    }
}
