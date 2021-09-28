using BinanceTrackerDesktop.Core.API;
using BinanceTrackerDesktop.Core.Validation.API;
using BinanceTrackerDesktop.Core.Validation.Extension;
using Google.Authenticator;
using System;
using System.Drawing;
using System.IO;

namespace BinanceTrackerDesktop.Core.User.Authentication.API
{
    public enum ValidateResult
    {
        Succesfully,
        Failed
    }

    public interface IUserAuthenticatorSystem
    {
        Image Authenticate(string title, string secret);

        ValidateResult ValidateTwoFactorPIN(string secret, string code);
    }

    public class UserAuthenticatorSystem : IUserAuthenticatorSystem
    {
        private const string IgnoringStringValue = "data:image/png;base64,";

        private const int MinCharactersOfPIN = 6;

        private const int NotSupportableCharactersCountOfPIN = 1;



        public Image Authenticate(string accountTitle, string secret)
        {
            if (!accountTitle.Rules()
                .ContentNotNullOrEmpty()
                .MinCharacters(NotSupportableCharactersCountOfPIN).IsSuccess)
                throw new FailedStringValidationException(nameof(accountTitle));

            if (!secret.Rules()
                .ContentNotNullOrEmpty()
                .MinCharacters(NotSupportableCharactersCountOfPIN).IsSuccess)
                throw new FailedStringValidationException(nameof(secret));

            SetupCode setupCode = new TwoFactorAuthenticator().GenerateSetupCode(ApplicationEnviroment.GlobalName, accountTitle.Trim(), secret, false);
            using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(setupCode.QrCodeSetupImageUrl.Replace(IgnoringStringValue, string.Empty))))
                return Image.FromStream(memoryStream);
        }

        public ValidateResult ValidateTwoFactorPIN(string secret, string code)
        {
            if (!secret.Rules()
                .ContentNotNullOrEmpty()
                .MinCharacters(MinCharactersOfPIN).IsSuccess)
                throw new FailedStringValidationException(nameof(secret));

            if (!code.Rules()
                .ContentNotNullOrEmpty()
                .MinCharacters(MinCharactersOfPIN).IsSuccess)
                throw new FailedStringValidationException(nameof(code));

            return new TwoFactorAuthenticator().ValidateTwoFactorPIN(secret, code) ? ValidateResult.Succesfully : ValidateResult.Failed;
        }
    }
}
