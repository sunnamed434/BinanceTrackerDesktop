using BinanceTrackerDesktop.Core.User.Data.API;
using System;

namespace BinanceTrackerDesktop.Core.User.Status.Extension
{
    public static class BinanceUserDataExtension
    {
        public static bool UserStartedApplicationFirstTime(this UserData source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return source.BestBalance == decimal.Zero;
        }
    }
}
