using BinanceTrackerDesktop.Authentication.TwoFactor.Exceptions;
using BinanceTrackerDesktop.Authentication.TwoFactor.Exceptions.ErrorCode;
using BinanceTrackerDesktop.Localizations.Data;
using BinanceTrackerDesktop.User.Authentication.System.Result;
using BinanceTrackerDesktop.Validators.String.Extension;
using Google.Authenticator;

namespace BinanceTrackerDesktop.User.Authentication.System;

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
            .ContentNotNullOrWhiteSpace()
            .MinCharacters(UnsupportedCharactersCount)
            .MaxCharacters(MaxAccountTitleCharacters)
            .ThrowIfFailed(new TwoFactorAuthenticationException(nameof(accountTitle), AuthenticationErrorCode.AccountTitle));

        secret.Rules()
            .ContentNotNullOrWhiteSpace()
            .MinCharacters(UnsupportedCharactersCount)
            .MaxCharacters(MaxSecretCharacters)
            .ThrowIfFailed(new TwoFactorAuthenticationException(nameof(secret), AuthenticationErrorCode.Secret));

        SetupCode setupCode = new TwoFactorAuthenticator().GenerateSetupCode(LocalizationData.Read().ApplicationName, accountTitle.Trim(), secret, false);
        using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(setupCode.QrCodeSetupImageUrl.Replace(IgnoringStringData, string.Empty))))
        {
            return Image.FromStream(memoryStream);
        }
    }

    public ValidateResult ValidateTwoFactor(string secret, string pin)
    {
        secret.Rules()
            .ContentNotNullOrWhiteSpace()
            .MinCharacters(MinCharacters)
            .ThrowIfFailed(new TwoFactorAuthenticationException(nameof(secret), AuthenticationErrorCode.Secret));

        pin.Rules()
            .ContentNotNullOrWhiteSpace()
            .MinCharacters(MinCharacters)
            .ThrowIfFailed(new TwoFactorAuthenticationException(nameof(pin), AuthenticationErrorCode.PIN));

        return new TwoFactorAuthenticator().ValidateTwoFactorPIN(secret, pin)
            ? ValidateResult.Successfully
            : ValidateResult.Failed;
    }
}
