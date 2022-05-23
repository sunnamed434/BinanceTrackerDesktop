using ProtoBuf;

namespace BinanceTrackerDesktop.Core.User.Authentication.Data
{
    [ProtoContract]
    public sealed class UserTwoFactorAuthenticationData
    {
        [ProtoMember(1)]
        public string Secret { get; set; }

        [ProtoMember(2)]
        public string LastActivityTime { get; set; }



        public UserTwoFactorAuthenticationData(string secret, DateTime lastActivityTime)
        {
            Secret = secret;
            LastActivityTime = lastActivityTime.ToString();
        }

        public UserTwoFactorAuthenticationData(string secret) : this(secret, DateTime.Now)
        {
        }

        public UserTwoFactorAuthenticationData() : this(null, new DateTime(1970, 1, 1))
        {
        }




        public DateTime GetLastActivityInDateTime()
        {
            return DateTime.Parse(LastActivityTime);
        }
    }
}
