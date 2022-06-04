using BinanceTrackerDesktop.Localizations.Translation;
using System.Reflection;

namespace BinanceTrackerDesktop.Localizations.Data;

public sealed class LocalizationData
{
    public string Settings { get; init; }

    public string Coins { get; init; }

    public string API { get; init; }

    public string OpenBinanceTracker { get; init; }

    public string QuitBinanceTracker { get; init; }

    public string EnableNotifications { get; init; }

    public string DisableNotifications { get; init; }

    public string NotificationsEnabled { get; init; }

    public string NotificationsDisabled { get; init; }

    public string TotalBalance { get; init; }



    private LocalizationData()
    {
        ITranslations translations = new Translations(new Localization.Localization());
        foreach (PropertyInfo propertyInfo in GetType().GetProperties())
        {
            propertyInfo.SetValue(this, translations.Translate(propertyInfo.Name));
        }
    }



    public static LocalizationData Read()
    {
        return new LocalizationData();
    }
}
