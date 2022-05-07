using BinanceTrackerDesktop.Core.User.Data.Save;

namespace BinanceTrackerDesktop.Core.User.Theme.Repositories
{
    public sealed class UserThemeRepository : IUserThemeRepository
    {
        public IUserDataSaveSystem UserDataSaveSystem { get; }



        public UserThemeRepository(IUserDataSaveSystem userDataSaveSystem)
        {
            UserDataSaveSystem = userDataSaveSystem ?? throw new ArgumentNullException(nameof(userDataSaveSystem));
        }



        public Themes.Theme GetTheme()
        {
            return UserDataSaveSystem.Read().Theme;
        }
    }
}
