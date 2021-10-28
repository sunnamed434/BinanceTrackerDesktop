using BinanceTrackerDesktop.Core.User.Data;
using System;

namespace BinanceTrackerDesktop.Core.User.Status.Extension
{
    public static class UserDataExtension
    {
        public static bool UserStartedApplicationFirstTime(this UserData source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return source.BestBalance == default;
        }
    }
}
