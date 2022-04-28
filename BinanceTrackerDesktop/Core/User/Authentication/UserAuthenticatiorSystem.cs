﻿using BinanceTrackerDesktop.Core.ApplicationInfo.Environment;
using BinanceTrackerDesktop.Core.Validators.String.Extension;
using Google.Authenticator;
using System;
using System.Drawing;
using System.IO;

namespace BinanceTrackerDesktop.Core.User.Authentication
{
    public enum ValidateResult
    {
        Successfully,
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
            accountTitle.Rules()
                .ContentNotNullOrEmpty()
                .MinCharacters(NotSupportableCharactersCountOfPIN)
                .ThrowIfFailed(nameof(accountTitle));

            secret.Rules()
                .ContentNotNullOrEmpty()
                .MinCharacters(NotSupportableCharactersCountOfPIN)
                .ThrowIfFailed(nameof(secret));

            SetupCode setupCode = new TwoFactorAuthenticator().GenerateSetupCode(ApplicationEnviroment.GlobalName, accountTitle.Trim(), secret, false);
            using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(setupCode.QrCodeSetupImageUrl.Replace(IgnoringStringValue, string.Empty))))
                return Image.FromStream(memoryStream);
        }

        public ValidateResult ValidateTwoFactorPIN(string secret, string code)
        {
            secret.Rules()
                .ContentNotNullOrEmpty()
                .MinCharacters(MinCharactersOfPIN)
                .ThrowIfFailed(nameof(secret));

            code.Rules()
                .ContentNotNullOrEmpty()
                .MinCharacters(MinCharactersOfPIN)
                .ThrowIfFailed(nameof(code));

            return new TwoFactorAuthenticator().ValidateTwoFactorPIN(secret, code) 
                ? ValidateResult.Successfully 
                : ValidateResult.Failed;
        }
    }
}
