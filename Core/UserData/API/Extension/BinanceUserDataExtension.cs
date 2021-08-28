namespace BinanceTrackerDesktop.Core.UserData.API.Extension
{
    public static class BinanceUserDataExtension
    {
        public static bool UserStartedApplicationFirstTime(this BinanceUserData userData)
        {
            return userData.Balance == decimal.Zero;
        }
    }
}
