using BinanceTrackerDesktop.Core.Themes.Theme;
using BinanceTrackerDesktop.Core.User.Authentication.Data;

namespace BinanceTrackerDesktop.Core.User.Data
{
    [Serializable]
    public sealed class UserData
    {
        public string Key;

        public string Secret;

        public string Currency;

        public UserTwoFactorAuthenticationData AuthenticationData;

        public Theme Theme;

        public decimal BestBalance;

        public bool? IsBalancesHiden;

        public bool? IsNotificationsEnabled;



        public bool IsNotificationsDisabled => IsNotificationsEnabled == false;
    }
}
