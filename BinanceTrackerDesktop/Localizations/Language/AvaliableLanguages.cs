using System.Collections.ObjectModel;
using static BinanceTrackerDesktop.DirectoryFiles.Controls.Localizations.LocalizationsDirectoryFilesControl;

namespace BinanceTrackerDesktop.Localizations.Language;

public sealed class AvaliableLanguages
{
    public static readonly ILanguage English = GetLanguage(Languages.English);

    public static readonly ILanguage Russian = GetLanguage(Languages.Russian);



    private static readonly IReadOnlyCollection<ILanguage> all = new ReadOnlyCollection<ILanguage>(new List<ILanguage>
    {
        new Language(Languages.English, RegisteredLocalizations.English),
        new Language(Languages.Russian, RegisteredLocalizations.Russian)
    });



    public static ILanguage GetLanguage(Languages name)
    {
        return all.FirstOrDefault(l => l.Name.Equals(name));
    }
}
