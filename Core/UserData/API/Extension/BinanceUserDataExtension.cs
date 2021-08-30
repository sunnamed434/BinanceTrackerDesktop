using System;

namespace BinanceTrackerDesktop.Core.UserData.API.Extension
{
    public static class BinanceUserDataExtension
    {
        public static bool UserStartedApplicationFirstTime(this BinanceUserData source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return source.Balance == decimal.Zero;
        }
    }
}
