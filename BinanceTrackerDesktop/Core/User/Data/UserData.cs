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

        public decimal BestBalance;

        public bool? IsBalancesHiden;

        public bool? IsNotificationsEnabled;



        public UserData(string key, string secret, string currency, UserTwoFactorAuthenticationData authenticationData)
        {
            Key = key;
            Secret = secret;
            Currency = currency;
            AuthenticationData = authenticationData;
        }

        public UserData(string key, string secret, string currency) : this(key, secret, currency, null)
        {
           
        }

        public UserData(string key, string secret, string currency, UserTwoFactorAuthenticationData authenticationData, decimal bestBalance, bool? isBalancesHiden, bool? isNotificationsEnabled) : this(key, secret, currency, authenticationData)
        {
            BestBalance = bestBalance;
            IsBalancesHiden = isBalancesHiden;
            IsNotificationsEnabled = isNotificationsEnabled;
        }

        public UserData()
            : this(key: null, secret: null, currency: null, null, bestBalance: default, isBalancesHiden: null, isNotificationsEnabled: null)
        {
        }



        public bool IsNotificationsDisabled => IsNotificationsEnabled == false;
    }
}
