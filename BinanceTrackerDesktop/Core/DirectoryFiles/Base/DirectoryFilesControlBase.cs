using BinanceTrackerDesktop.Core.DirectoryFiles.Item;
using BinanceTrackerDesktop.Core.DirectoryFiles.Utility;

namespace BinanceTrackerDesktop.Core.DirectoryFiles.Base
{
    public abstract class DirectoryFilesControlBase<TDirectoryFileItem> 
        : IDirectoryFilesControl<TDirectoryFileItem> where TDirectoryFileItem : IDirectoryFileItem
    {
        public abstract string FolderPath { get; }

        public abstract IEnumerable<TDirectoryFileItem> Files { get; }

        public virtual IEnumerable<string> FileExtensions => new List<string>();



        public virtual TDirectoryFileItem GetDirectoryFile(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            return Files.FirstOrDefault(i => i.FileName.Contains(name));
        }

        public virtual IEnumerable<string> GetAllFilePathFromDirectory()
        {
            Directory.CreateDirectory(FolderPath);

            string[] filesPaths = Directory.GetFiles(FolderPath);
            for (int i = 0; i < filesPaths.Length; i++)
                if (FilePathUtility.TryGetExtensionOf(filesPaths[i], out string extension) && FileExtensions.Contains(extension))
                    yield return filesPaths[i];
        }
    }
}
