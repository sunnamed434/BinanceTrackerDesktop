using BinanceTrackerDesktop.Core.Localizations.Language;
using BinanceTrackerDesktop.Core.User.Authentication.Data;
using ProtoBuf;

namespace BinanceTrackerDesktop.Core.User.Data
{
    [ProtoContract]
    public sealed class UserData
    {
        [ProtoMember(1)]
        public string Key { get; set; }

        [ProtoMember(2)]
        public string Secret { get; set; }

        [ProtoMember(3)]
        public string Currency { get; set; }

        [ProtoMember(4)]
        public UserTwoFactorAuthenticationData AuthenticationData { get; set; }

        [ProtoMember(5)]
        public Themes.Theme Theme { get; set; }

        [ProtoMember(6)]
        public Language Language { get; set; }

        [ProtoMember(7)]
        public decimal? BestBalance { get; set; }

        [ProtoMember(8)]
        public bool? IsBalancesHiden { get; set; }

        [ProtoMember(9)]
        public bool IsNotificationsEnabled { get; set; }

        [ProtoMember(10)]
        public bool IsUserStartedApplicationFirstTime { get; set; }



        public bool IsNotificationsDisabled => IsNotificationsEnabled == false;

        public bool HasAuthenticationData => AuthenticationData != null;
    }
}
