using BinanceTrackerDesktop.Core.DirectoryFiles.Controls.UserData;
using BinanceTrackerDesktop.DirectoryFiles.Controls.Images;
using BinanceTrackerDesktop.DirectoryFiles.Controls.Localizations;
using BinanceTrackerDesktop.DirectoryFiles.Controls.Themes;

namespace BinanceTrackerDesktop.DirectoryFiles.Directories;

public sealed class ApplicationDirectories
{
    public sealed class Resources
    {
        public static readonly ImagesDirectoryFilesControl Images = new();

        public static readonly UserDataDirectoryFilesControl User = new();

        public static readonly ThemesDirectoryFilesControl Themes = new();

        public static readonly LocalizationsDirectoryFilesControl Localizations = new();
    }
}
