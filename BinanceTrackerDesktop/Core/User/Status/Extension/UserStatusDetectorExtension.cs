using BinanceTrackerDesktop.Core.User.Client;
using BinanceTrackerDesktop.Core.User.Control;
using System;

namespace BinanceTrackerDesktop.Core.User.Status.Extension
{
    public static class UserStatusDetectorExtension
    {
        public static IUserStatus GetUserStatus(this UserClient source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return new UserStatusDetector(source.SaveDataSystem, source.Wallet).GetStatus();
        }
    }
}
