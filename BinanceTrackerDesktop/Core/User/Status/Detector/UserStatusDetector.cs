using BinanceTrackerDesktop.Core.User.Control;
using BinanceTrackerDesktop.Core.User.Data.Save;
using BinanceTrackerDesktop.Core.User.Status.Beginner;
using BinanceTrackerDesktop.Core.User.Status.Standart;
using BinanceTrackerDesktop.Core.User.Wallet;

namespace BinanceTrackerDesktop.Core.User.Status.Detector
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
            return this.userDataSaveSystem.Read().IsUserStartedApplicationFirstTime
                ? new UserBeginnerStatus(this.userDataSaveSystem, this.wallet)
                : new UserStandartStatus(this.userDataSaveSystem, this.wallet);
        }
    }
}
