using BinanceTrackerDesktop.User.Data.Save.Binary;
using BinanceTrackerDesktop.User.Data.Value.Repositories.Authentication;
using BinanceTrackerDesktop.User.Data.Value.Repositories.Language;
using BinanceTrackerDesktop.User.Data.Value.Repositories.Notifications;
using BinanceTrackerDesktop.User.Data.Value.Repositories.Theme;

namespace BinanceTrackerDesktop.User.Data.Value;

public sealed class UserDataValues
{
    public static readonly HasAuthenticationDataUserDataValueRepository HasAuthenticationData = new(new BinaryUserDataSaveSystem());

    public static readonly LanguageUserDataValueRepository Language = new(new BinaryUserDataSaveSystem());

    public static readonly ThemeUserDataValueRepository Theme = new(new BinaryUserDataSaveSystem());

    public static readonly BalancesHidenUserDataValueRepository BalancesHiden = new(new BinaryUserDataSaveSystem());

    public static readonly NotificationsDisabledUserDataValueRepository NotificationsDisabled = new(new BinaryUserDataSaveSystem());
}
