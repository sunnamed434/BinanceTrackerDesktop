using BinanceTrackerDesktop.Core.Localizations.Language;
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

        public Themes.Theme Theme;

        public ILanguage Language;

        public decimal? BestBalance;

        public bool? IsBalancesHiden;

        public bool IsNotificationsEnabled;

        public bool IsUserStartedApplicationFirstTime;



        public bool IsNotificationsDisabled => IsNotificationsEnabled == false;

        public bool HasAuthenticationData => AuthenticationData != null;
    }
}
