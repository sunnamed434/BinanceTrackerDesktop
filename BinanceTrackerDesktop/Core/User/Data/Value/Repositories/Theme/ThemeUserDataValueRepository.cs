using BinanceTrackerDesktop.Core.User.Data.Save;

namespace BinanceTrackerDesktop.Core.User.Data.Value.Repositories.Language
{
    public sealed class ThemeUserDataValueRepository : IUserDataValueRepository<Themes.Theme>
    {
        public IUserDataSaveSystem UserDataSaveSystem { get; }



        public ThemeUserDataValueRepository(IUserDataSaveSystem userDataSaveSystem)
        {
            UserDataSaveSystem = userDataSaveSystem ?? throw new ArgumentNullException(nameof(userDataSaveSystem));
        }



        public Themes.Theme GetValue()
        {
            return UserDataSaveSystem.Read().Theme;
        }
    }
}
