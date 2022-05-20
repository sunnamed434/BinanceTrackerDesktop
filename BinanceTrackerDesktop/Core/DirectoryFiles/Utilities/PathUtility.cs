namespace BinanceTrackerDesktop.Core.DirectoryFiles.Utilities
{
    public sealed class PathUtility
    {
        public static bool TryGetExtensionFromPath(string path, out string result)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (File.Exists(path) == false)
            {
                throw new FileNotFoundException(nameof(path));
            }

            string fileExtension = Path.GetExtension(path);
            return string.IsNullOrWhiteSpace(result = fileExtension) == false;
        }

        public static bool TryGetExtensionFromPathAndCompareIt(string path, Predicate<string> predicate)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(nameof(path));
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (TryGetExtensionFromPath(path, out string extension))
            {
                return predicate.Invoke(extension);
            }

            return false;
        }
    }
}
