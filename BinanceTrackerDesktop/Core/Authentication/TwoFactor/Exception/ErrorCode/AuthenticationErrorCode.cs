namespace BinanceTrackerDesktop.Core.Authentication.TwoFactor.Exception.ErrorCode
{
    public enum AuthenticationErrorCode : byte
    {
        Unknown = 0,
        Key = 1,
        Secret = 2,
        AccountTitle = 3,
        PIN = 4,
    }
}
