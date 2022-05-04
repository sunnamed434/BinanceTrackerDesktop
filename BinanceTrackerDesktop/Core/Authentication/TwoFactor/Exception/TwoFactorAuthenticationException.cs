using BinanceTrackerDesktop.Core.Authentication.TwoFactor.Exception.ErrorCode;

namespace BinanceTrackerDesktop.Core.Authentication.TwoFactor.Exception
{
    public sealed class TwoFactorAuthenticationException : System.Exception
    {
        public readonly AuthenticationErrorCode ErrorCode;



        public TwoFactorAuthenticationException(string argument, AuthenticationErrorCode errorCode) 
            : base(string.Format("String validation failed: {0}", argument))
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
}
