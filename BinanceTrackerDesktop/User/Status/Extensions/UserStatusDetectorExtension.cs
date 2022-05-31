using BinanceTrackerDesktop.User.Client;
using BinanceTrackerDesktop.User.Status.API;
using BinanceTrackerDesktop.User.Status.Detector;

namespace BinanceTrackerDesktop.User.Status.Extensions;

public static class UserStatusDetectorExtension
{
    public static IUserStatus GetUserStatus(this UserClient source)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        return new UserStatusDetector(source.SaveDataSystem, source.Wallet).GetStatus();
    }
}
