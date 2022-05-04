using BinanceTrackerDesktop.Core.DirectoryFiles.Format;
using BinanceTrackerDesktop.Core.DirectoryFiles.Paths;
using System.Text;
using static BinanceTrackerDesktop.Core.DirectoryFiles.Control.DirectoryFilesControl;

namespace BinanceTrackerDesktop.Core.User.Data.FileData
{
    public sealed class UserDataFile
    {
        public static readonly string FullPath = Path.Combine(ApplicationDirectoryPaths.User, new StringBuilder()
            .Append(RegisteredData.UserFile)
            .Append(FilesFormatExtensions.DAT)
            .ToString());
    }
}
