using BinanceTrackerDesktop.DirectoryFiles.Base;
using BinanceTrackerDesktop.DirectoryFiles.Formats;
using BinanceTrackerDesktop.DirectoryFiles.Item;
using BinanceTrackerDesktop.DirectoryFiles.Paths;
using static BinanceTrackerDesktop.DirectoryFiles.Controls.Images.ImagesDirectoryFilesControl;

namespace BinanceTrackerDesktop.DirectoryFiles.Controls.Images;

public sealed class ImagesDirectoryFilesControl : DirectoryFilesControlBase<DirectoryImageItem>
{
    public override string FolderPath => ApplicationDirectoryPaths.Images;

    public override IEnumerable<string> FilesExtensions => new List<string>
    {
        FilesFormatExtensions.ICO,
        FilesFormatExtensions.JPG,
        FilesFormatExtensions.PNG
    };

    public override IEnumerable<DirectoryImageItem> Files { get; }



    public ImagesDirectoryFilesControl()
    {
        List<DirectoryImageItem> items = new List<DirectoryImageItem>();
        foreach (string filePath in GetAllFilePathFromDirectory())
        {
            items.Add(new DirectoryImageItem(filePath));
        }

        Files = items;
    }



    public sealed class DirectoryImageItem : IResultableDirectoryFileItem
    {
        public object Result { get; }

        public string FileName { get; }

        public string FilePath { get; }



        public DirectoryImageItem(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException(nameof(filePath));
            }

            if (File.Exists(filePath) == false)
            {
                throw new FileNotFoundException(nameof(filePath));
            }

            Result = Image.FromFile(filePath);
            FileName = Path.GetFileName(filePath);
            FilePath = filePath;
        }



        public TResult GetResult<TResult>()
        {
            return (TResult)Result;
        }

        public Image GetImage()
        {
            return GetResult<Image>();
        }

        public Icon GetIcon()
        {
            return Icon.FromHandle(GetResult<Bitmap>().GetHicon());
        }
    }

    public sealed class RegisteredImages
    {
        public const string ApplicationIcon = "app";
    }
}
