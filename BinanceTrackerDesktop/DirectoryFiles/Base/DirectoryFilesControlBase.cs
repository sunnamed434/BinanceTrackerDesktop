using BinanceTrackerDesktop.DirectoryFiles.Item;
using BinanceTrackerDesktop.DirectoryFiles.Utilities;

namespace BinanceTrackerDesktop.DirectoryFiles.Base;

public abstract class DirectoryFilesControlBase<TDirectoryFileItem> :
    DirectoryFilesControlBase,
    IDirectoryFilesControl<TDirectoryFileItem> where TDirectoryFileItem : IDirectoryFileItem
{
    public abstract IEnumerable<TDirectoryFileItem> Files { get; }



    public virtual TDirectoryFileItem GetDirectoryFile(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(nameof(name));
        }

        return Files.FirstOrDefault(i => i.FileName.Contains(name));
    }
}

public abstract class DirectoryFilesControlBase :
    IDirectoryFilesControl
{
    public abstract string FolderPath { get; }

    public virtual IEnumerable<string> FilesExtensions { get; }



    public virtual IEnumerable<string> GetAllFilePathFromDirectory()
    {
        Directory.CreateDirectory(FolderPath);

        string[] filesPaths = Directory.GetFiles(FolderPath);
        for (int i = 0; i < filesPaths.Length; i++)
        {
            if (PathUtility.TryGetExtensionFromPathAndCompareIt(filesPaths[i], (e) => FilesExtensions.Contains(e)))
            {
                yield return filesPaths[i];
            }
        }
    }
}
