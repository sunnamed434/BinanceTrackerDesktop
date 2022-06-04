using BinanceTrackerDesktop.Localizations.Language;
using BinanceTrackerDesktop.User.Data.Save;

namespace BinanceTrackerDesktop.User.Data.Value.Repositories.Language;

public sealed class LanguageUserDataValueRepository : UserDataValueRepository<ILanguage>
{
    public LanguageUserDataValueRepository(IUserDataSaveSystem userDataSaveSystem) : base(userDataSaveSystem)
    {
    }



    public override ILanguage GetValue()
    {
        return UserDataSaveSystem.Read().Language;
    }
}
