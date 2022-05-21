using BinanceTrackerDesktop.Core.User.Data.Save;
using BinanceTrackerDesktop.Core.User.Data.Save.Binary;
using BinanceTrackerDesktop.Core.User.Data.Value.Repositories.Authentication;
using BinanceTrackerDesktop.Core.User.Data.Value.Repositories.Language;

namespace BinanceTrackerDesktop.Core.User.Data.Value
{
    public sealed class UserDataValues
    {
        public static readonly HasAuthenticationDataUserDataValueRepository HasAuthenticationData = new HasAuthenticationDataUserDataValueRepository(SaveSystem);

        public static readonly LanguageUserDataValueRepository Language = new LanguageUserDataValueRepository(SaveSystem);

        public static readonly ThemeUserDataValueRepository Theme = new ThemeUserDataValueRepository(SaveSystem);

        private static readonly IUserDataSaveSystem SaveSystem = new BinaryUserDataSaveSystem();
    }
}
