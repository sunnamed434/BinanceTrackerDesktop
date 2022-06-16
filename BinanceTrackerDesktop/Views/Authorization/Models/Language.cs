using BinanceTrackerDesktop.Localizations.Language;
using BinanceTrackerDesktop.Localizations.Language.Names;

namespace BinanceTrackerDesktop.Views.Authorization.Models
{
    public sealed class Language
    {
        public string DisplayName;

        public Image Image;

        public Languages Name;



        public Language(string displayName, Image image)
        {
            if (string.IsNullOrWhiteSpace(displayName))
            {
                throw new ArgumentException(nameof(displayName));
            }

            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            DisplayName = displayName;
            Image = image;
            Name = LanguageNames.GetFromName(displayName);
        }
    }
}
