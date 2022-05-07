namespace BinanceTrackerDesktop.Core.DirectoryFiles.Utility
{
    public sealed class FilePathUtility
    {
        public static bool TryGetExtensionOf(string path, out string result)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            if (!File.Exists(path))
                throw new FileNotFoundException(nameof(path));

            string fileExtension = Path.GetExtension(path);
            return !string.IsNullOrEmpty(result = fileExtension);
        }
    }
}
