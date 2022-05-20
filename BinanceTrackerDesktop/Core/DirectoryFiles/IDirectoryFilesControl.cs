using BinanceTrackerDesktop.Core.DirectoryFiles.Item;

namespace BinanceTrackerDesktop.Core.DirectoryFiles
{
    public interface IDirectoryFilesControl<TDirectoryFileItem> : IDirectoryFilesControl where TDirectoryFileItem : IDirectoryFileItem
    {
        IEnumerable<TDirectoryFileItem> Files { get; }



        TDirectoryFileItem GetDirectoryFile(string name);
    }

    public interface IDirectoryFilesControl
    {
        string FolderPath { get; }

        IEnumerable<string> FilesExtensions { get; }



        IEnumerable<string> GetAllFilePathFromDirectory();
    }
}
