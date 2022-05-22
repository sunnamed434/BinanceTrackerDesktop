using BinanceTrackerDesktop.Core.Views.Authorization.Exceptions.ErrorCode;

namespace BinanceTrackerDesktop.Core.Views.Authorization.Exceptions
{
    public sealed class AuthorizationException : Exception
    {
        public readonly AuthorizationErrorCode ErrorCode;



        public AuthorizationException(string argument, AuthorizationErrorCode errorCode)
            : base(string.Format("Authorization failed: {0}", argument))
        {
            ErrorCode = errorCode;
        }

        public AuthorizationException(string argument)
            : this(argument, AuthorizationErrorCode.Unkown)
        {
        }

        public AuthorizationException()
            : this(string.Empty)
        {
        }
    }
}
