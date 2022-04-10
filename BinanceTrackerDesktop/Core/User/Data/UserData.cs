using System;

namespace BinanceTrackerDesktop.Core.User.Data
{
    [Serializable]
    public sealed class UserData
    {
        public string Key;

        public string Secret;

        public string Currency;

        public decimal BestBalance;

        public bool? IsBalancesHiden;

        public bool? IsNotificationsEnabled;



        public UserData(string key, string secret, string currency)
        {
            Key = key;
            Secret = secret;
            Currency = currency;
        }

        public UserData(string key, string secret, string currency, decimal bestBalance, bool? isBalancesHiden, bool? isNotificationsEnabled) : this(key, secret, currency)
        {
            BestBalance = bestBalance;
            IsBalancesHiden = isBalancesHiden;
            IsNotificationsEnabled = isNotificationsEnabled;
        }

        public UserData() : this(key: null, secret: null, currency: null, bestBalance: default, isBalancesHiden: null, isNotificationsEnabled: null)
        {

        }



        public bool IsNotificationsDisabled => IsNotificationsEnabled == false;
    }
}
