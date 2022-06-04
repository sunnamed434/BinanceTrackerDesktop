using BinanceTrackerDesktop.User.Data.Save;

namespace BinanceTrackerDesktop.User.Data.Value.Repositories.Theme;

public sealed class ThemeUserDataValueRepository : UserDataValueRepository<Themes.Theme>
{
    public ThemeUserDataValueRepository(IUserDataSaveSystem userDataSaveSystem) : base(userDataSaveSystem)
    {
    }



    public override Themes.Theme GetValue()
    {
        return UserDataSaveSystem.Read().Theme;
    }
}
