using BinanceTrackerDesktop.Localizations.Language;
using BinanceTrackerDesktop.User.Data.Save;

namespace BinanceTrackerDesktop.User.Data.Value.Repositories.Language;

public sealed class LanguageUserDataValueRepository : IUserDataValueRepository<ILanguage>
{
    public LanguageUserDataValueRepository(IUserDataSaveSystem userDataSaveSystem)
    {
        UserDataSaveSystem = userDataSaveSystem ?? throw new ArgumentNullException(nameof(userDataSaveSystem));
    }



    public IUserDataSaveSystem UserDataSaveSystem { get; }



    public ILanguage GetValue()
    {
        return UserDataSaveSystem.Read().Language;
    }
}
