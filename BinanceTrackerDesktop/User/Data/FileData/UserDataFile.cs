using BinanceTrackerDesktop.DirectoryFiles.Formats;
using BinanceTrackerDesktop.DirectoryFiles.Paths;
using System.Text;
using static BinanceTrackerDesktop.Core.DirectoryFiles.Controls.UserData.UserDataDirectoryFilesControl;

namespace BinanceTrackerDesktop.User.Data.FileData;

public sealed class UserDataFile
{
    public static readonly string FullPath = Path.Combine(ApplicationDirectoryPaths.UserData, new StringBuilder()
        .Append(RegisteredData.UserFile)
        .Append(FilesFormatExtensions.BIN)
        .ToString());
}
