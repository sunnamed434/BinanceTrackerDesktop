using BinanceTrackerDesktop.DirectoryFiles.Base;
using BinanceTrackerDesktop.DirectoryFiles.Formats;
using BinanceTrackerDesktop.DirectoryFiles.Item;
using BinanceTrackerDesktop.DirectoryFiles.Paths;
using static BinanceTrackerDesktop.Core.DirectoryFiles.Controls.UserData.UserDataDirectoryFilesControl;

namespace BinanceTrackerDesktop.Core.DirectoryFiles.Controls.UserData;

public sealed class UserDataDirectoryFilesControl : DirectoryFilesControlBase<DirectoryDataItem>
{
    public override string FolderPath => ApplicationDirectoryPaths.UserData;

    public override IEnumerable<string> FilesExtensions => new List<string>
    {
        FilesFormatExtensions.DAT
    };

    public override IEnumerable<DirectoryDataItem> Files { get; }



    public UserDataDirectoryFilesControl()
    {
        Directory.CreateDirectory(FolderPath);

        List<DirectoryDataItem> items = new List<DirectoryDataItem>();
        foreach (string filePath in GetAllFilePathFromDirectory())
        {
            items.Add(new DirectoryDataItem(filePath));
        }

        Files = items;
    }



    public sealed class DirectoryDataItem : IDirectoryFileItem
    {
        public string FilePath { get; }

        public string FileName { get; }



        public DirectoryDataItem(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException(nameof(filePath));
            }

            if (File.Exists(filePath) == false)
            {
                throw new FileNotFoundException(nameof(filePath));
            }

            FileName = Path.GetFileName(filePath);
            FilePath = filePath;
        }
    }

    public sealed class RegisteredData
    {
        public const string UserFile = "userdata";
    }
}
