using BinanceTrackerDesktop.User.Data.Save;
using BinanceTrackerDesktop.User.Status.API;
using BinanceTrackerDesktop.User.Status.Beginner;
using BinanceTrackerDesktop.User.Status.Standart;
using BinanceTrackerDesktop.User.Wallet;

namespace BinanceTrackerDesktop.User.Status.Detector
{
    public sealed class UserStatusDetector : IUserStatusDetector
    {
        private readonly IUserDataSaveSystem userDataSaveSystem;

        private readonly UserWallet wallet;



        public UserStatusDetector(IUserDataSaveSystem userDataSaveSystem, UserWallet wallet)
        {
            this.userDataSaveSystem = userDataSaveSystem ?? throw new ArgumentNullException(nameof(userDataSaveSystem));
            this.wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
        }



        public IUserStatus GetStatus()
        {
            return userDataSaveSystem.Read().IsUserStartedApplicationFirstTime
                ? new UserBeginnerStatus(userDataSaveSystem, wallet)
                : new UserStandartStatus(userDataSaveSystem, wallet);
        }
    }
}
