using BinanceTrackerDesktop.Core.DirectoryFiles.API;

namespace BinanceTrackerDesktop.Core.DirectoryFiles.Extension
{
    public static class FilePathExtension
    {
        public static bool IsIcon(this string source)
        {
            return FilePathUtility.TryGetExtensionOf(source, out string fileExtension) && fileExtension == FileExtensions.Icon;
        }

        public static bool IsDat(this string source)
        {
            return FilePathUtility.TryGetExtensionOf(source, out string fileExtension) && fileExtension == FileExtensions.Dat;
        }
    }
}
