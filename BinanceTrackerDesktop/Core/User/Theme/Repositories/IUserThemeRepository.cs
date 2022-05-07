using BinanceTrackerDesktop.Core.User.Data.Save;

namespace BinanceTrackerDesktop.Core.User.Theme.Repositories
{
    public interface IUserThemeRepository
    {
        IUserDataSaveSystem UserDataSaveSystem { get; }



        Themes.Theme GetTheme();
    }
}