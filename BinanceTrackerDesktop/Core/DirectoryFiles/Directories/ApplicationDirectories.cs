using BinanceTrackerDesktop.Core.DirectoryFiles.Controls.Images;
using BinanceTrackerDesktop.Core.DirectoryFiles.Controls.Localizations;
using BinanceTrackerDesktop.Core.DirectoryFiles.Controls.Themes;
using BinanceTrackerDesktop.Core.DirectoryFiles.Controls.UserData;

namespace BinanceTrackerDesktop.Core.DirectoryFiles.Directories
{
    public sealed class ApplicationDirectories
    {
        public sealed class Resources
        {
            public static readonly ImagesDirectoryFilesControl Images = new ImagesDirectoryFilesControl();

            public static readonly UserDataDirectoryFilesControl User = new UserDataDirectoryFilesControl();

            public static readonly ThemesDirectoryFilesControl Themes = new ThemesDirectoryFilesControl();

            public static readonly LocalizationsDirectoryFilesControl Localizations = new LocalizationsDirectoryFilesControl();
        }
    }
}
