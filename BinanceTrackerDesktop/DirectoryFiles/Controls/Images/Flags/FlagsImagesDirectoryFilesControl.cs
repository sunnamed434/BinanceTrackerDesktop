using BinanceTrackerDesktop.DirectoryFiles.Base;
using BinanceTrackerDesktop.DirectoryFiles.Formats;
using BinanceTrackerDesktop.DirectoryFiles.Paths;
using static BinanceTrackerDesktop.DirectoryFiles.Controls.Images.ImagesDirectoryFilesControl;

namespace BinanceTrackerDesktop.DirectoryFiles.Controls.Images.Flags;

public sealed class FlagsImagesDirectoryFilesControl : DirectoryFilesControlBase<DirectoryImageItem>
{
    public override string FolderPath => ApplicationDirectoryPaths.FlagsImages;

    public override IEnumerable<string> FilesExtensions => new List<string>
    {
        FilesFormatExtensions.PNG
    };

    public override IEnumerable<DirectoryImageItem> Files { get; }



    public FlagsImagesDirectoryFilesControl()
    {
        List<DirectoryImageItem> items = new List<DirectoryImageItem>();
        foreach (string filePath in GetAllFilePathFromDirectory())
        {
            items.Add(new DirectoryImageItem(filePath));
        }

        Files = items;
    }
}
