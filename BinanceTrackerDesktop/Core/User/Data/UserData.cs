using System;

namespace BinanceTrackerDesktop.Core.User.Data
{
    [Serializable]
    public sealed class UserData
    {
        public string Key;

        public string Secret;

        public decimal BestBalance;

        public bool? BalancesHiden;

        public bool? NotificationsEnabled;



        public UserData(string key, string secret)
        {
            Key = key;
            Secret = secret;
        }

        public UserData(string key, string secret, decimal bestBalance, bool? balancesHiden, bool? notificationsEnabled) : this(key, secret)
        {
            BestBalance = bestBalance;
            BalancesHiden = balancesHiden;
            NotificationsEnabled = notificationsEnabled;
        }

        public UserData() : this(null, null, default, null, null)
        {

        }
    }
}
