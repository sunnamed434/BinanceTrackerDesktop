using BinanceTrackerDesktop.Core.User.Data.Save;
using BinanceTrackerDesktop.Core.User.Data.Save.Binary;
using BinanceTrackerDesktop.Core.User.Data.Value.Repositories.Authentication;
using BinanceTrackerDesktop.Core.User.Data.Value.Repositories.Language;

namespace BinanceTrackerDesktop.Core.User.Data.Value
{
    public sealed class UserDataValues
    {
        public static readonly HasAuthenticationDataUserDataValueRepository HasAuthenticationData = new HasAuthenticationDataUserDataValueRepository(new BinaryUserDataSaveSystem());

        public static readonly LanguageUserDataValueRepository Language = new LanguageUserDataValueRepository(new BinaryUserDataSaveSystem());

        public static readonly ThemeUserDataValueRepository Theme = new ThemeUserDataValueRepository(new BinaryUserDataSaveSystem());

        public static readonly BalancesHidenUserDataValueRepository BalancesHiden = new BalancesHidenUserDataValueRepository(new BinaryUserDataSaveSystem());
    }
}
