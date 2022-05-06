using BinanceTrackerDesktop.Core.User.Client;
using BinanceTrackerDesktop.Core.User.Control;
using BinanceTrackerDesktop.Core.User.Status.Detector;

namespace BinanceTrackerDesktop.Core.User.Status.Extensions
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
