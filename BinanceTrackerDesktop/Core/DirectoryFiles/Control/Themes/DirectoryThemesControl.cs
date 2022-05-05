using BinanceTrackerDesktop.Core.DirectoryFiles.Base;
using BinanceTrackerDesktop.Core.DirectoryFiles.Format;
using BinanceTrackerDesktop.Core.DirectoryFiles.Item;
using BinanceTrackerDesktop.Core.DirectoryFiles.Paths;
using static BinanceTrackerDesktop.Core.DirectoryFiles.Control.Themes.DirectoryThemesControl;

namespace BinanceTrackerDesktop.Core.DirectoryFiles.Control.Themes
{
    public sealed class DirectoryThemesControl : DirectoryFilesControlBase<DirectoryThemeFileItem>
    {
        public override string FolderPath => ApplicationDirectoryPaths.Themes;

        public override IEnumerable<string> FileExtensions => new List<string>
        {
            FilesFormatExtensions.THEME,
        };

        public override IEnumerable<DirectoryThemeFileItem> Files { get; }



        public DirectoryThemesControl()
        {
            Directory.CreateDirectory(FolderPath);

            List<DirectoryThemeFileItem> items = new List<DirectoryThemeFileItem>();
            foreach (string filePath in base.GetAllFilePathFromDirectory())
                items.Add(new DirectoryThemeFileItem(filePath));

            Files = items;
        }



        public sealed class DirectoryThemeFileItem : IDirectoryFileItem
        {
            public object Result { get; }

            public string FilePath { get; }

            public string FileName { get; }



            public DirectoryThemeFileItem(string filePath)
            {
                if (string.IsNullOrWhiteSpace(filePath))
                    throw new ArgumentNullException(nameof(filePath));

                if (File.Exists(filePath) == false)
                    throw new FileNotFoundException(nameof(filePath));

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

        public sealed class RegisteredThemes
        {
            public const string DarkTheme = "dark";

            public const string LightTheme = "light";
        }
    }
}
