using BinanceTrackerDesktop.Authentication.TwoFactor.Exceptions.ErrorCode;

namespace BinanceTrackerDesktop.Authentication.TwoFactor.Exceptions;

public sealed class TwoFactorAuthenticationException : Exception
{
    public readonly AuthenticationErrorCode ErrorCode;



    public TwoFactorAuthenticationException(string argument, AuthenticationErrorCode errorCode)
        : base(string.Format("Two factor authentication failed: {0}", argument))
    {
        ErrorCode = errorCode;
    }

    public TwoFactorAuthenticationException(string argument)
        : this(argument, AuthenticationErrorCode.Unknown)
    {
    }

    public TwoFactorAuthenticationException()
        : this(string.Empty)
    {
    }
}
