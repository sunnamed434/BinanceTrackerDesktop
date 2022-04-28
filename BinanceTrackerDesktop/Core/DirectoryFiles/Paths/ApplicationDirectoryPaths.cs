using System.IO;

namespace BinanceTrackerDesktop.Core.DirectoryFiles.Paths
{
    public sealed class ApplicationDirectoryPaths
    {
        public static readonly string Resources = nameof(Resources);

        public static readonly string User = Path.Combine(Resources, nameof(User));

        public static readonly string Images = Path.Combine(Resources, nameof(Images));
    }
}
