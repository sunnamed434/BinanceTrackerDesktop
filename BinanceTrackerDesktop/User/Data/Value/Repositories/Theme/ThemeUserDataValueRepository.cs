using BinanceTrackerDesktop.User.Data.Save;

namespace BinanceTrackerDesktop.User.Data.Value.Repositories.Theme;

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
