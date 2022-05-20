using BinanceTrackerDesktop.Core.DirectoryFiles.Base;
using BinanceTrackerDesktop.Core.DirectoryFiles.Formats;
using BinanceTrackerDesktop.Core.DirectoryFiles.Item;
using BinanceTrackerDesktop.Core.DirectoryFiles.Paths;
using static BinanceTrackerDesktop.Core.DirectoryFiles.Controls.Localizations.LocalizationsDirectoryFilesControl;

namespace BinanceTrackerDesktop.Core.DirectoryFiles.Controls.Localizations
{
    public sealed class LocalizationsDirectoryFilesControl : DirectoryFilesControlBase<LocalizationDirectoryFileItem>
    {
        public override string FolderPath => ApplicationDirectoryPaths.Localizations;

        public override IEnumerable<string> FilesExtensions => new List<string>
        {
            FilesFormatExtensions.JSON,
        };

        public override IEnumerable<LocalizationDirectoryFileItem> Files { get; }



        public LocalizationsDirectoryFilesControl()
        {
            Directory.CreateDirectory(FolderPath);

            List<LocalizationDirectoryFileItem> items = new List<LocalizationDirectoryFileItem>();
            foreach (string filePath in base.GetAllFilePathFromDirectory())
            {
                items.Add(new LocalizationDirectoryFileItem(filePath));
            }

            Files = items;
        }



        public sealed class LocalizationDirectoryFileItem : IResultableDirectoryFileItem
        {
            public object Result { get; }

            public string FilePath { get; }

            public string FileName { get; }



            public LocalizationDirectoryFileItem(string filePath)
            {
                if (string.IsNullOrWhiteSpace(filePath))
                {
                    throw new ArgumentNullException(nameof(filePath));
                }

                if (File.Exists(filePath) == false)
                {
                    throw new FileNotFoundException(nameof(filePath));
                }

                Result = File.ReadAllText(filePath);
                FileName = Path.GetFileName(filePath);
                FilePath = filePath;
            }



            public TResult GetResult<TResult>()
            {
                return (TResult)Result;
            }

            public string GetStringResult()
            {
                return GetResult<string>();
            }
        }

        public sealed class RegisteredLocalizations
        {
            public const string English = "en";

            public const string Russian = "ru";
        }
    }
}
