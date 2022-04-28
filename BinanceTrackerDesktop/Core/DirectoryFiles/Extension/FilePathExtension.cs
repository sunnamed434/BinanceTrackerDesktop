using BinanceTrackerDesktop.Core.DirectoryFiles.Format;
using BinanceTrackerDesktop.Core.DirectoryFiles.Utility;

namespace BinanceTrackerDesktop.Core.DirectoryFiles.Extension
{
    public static class FilePathExtension
    {
        public static bool IsImage(this string source)
        {
            return FilePathUtility.TryGetExtensionOf(source, out string fileExtension) &&
                source.IsIcon() ||
                fileExtension == FilesFormatExtensions.JPG ||
                fileExtension == FilesFormatExtensions.PNG;
        }

        public static bool IsIcon(this string source)
        {
            return FilePathUtility.TryGetExtensionOf(source, out string fileExtension) && fileExtension == FilesFormatExtensions.ICO;
        }

        public static bool IsDat(this string source)
        {
            return FilePathUtility.TryGetExtensionOf(source, out string fileExtension) && 
                fileExtension == FilesFormatExtensions.DAT;
        }
    }
}
