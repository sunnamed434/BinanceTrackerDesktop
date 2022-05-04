namespace BinanceTrackerDesktop.Core.User.Authentication.Data
{
    [Serializable]
    public sealed class UserTwoFactorAuthenticationData
    {
        public string Secret;

        public string LastActivityTime;



        public UserTwoFactorAuthenticationData(string secret, string lastUsageTime)
        {
            Secret = secret;
            LastActivityTime = lastUsageTime;
        }

        public UserTwoFactorAuthenticationData(string secret) : this(secret, DateTime.Now.ToString())
        {
        }

        public UserTwoFactorAuthenticationData() : this(null, null)
        {
        }
    }
}
