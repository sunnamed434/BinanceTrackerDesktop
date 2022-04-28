namespace BinanceTrackerDesktop.Core.DirectoryFiles.Item
{
    public interface IDirectoryFileItem
    {
        object Result { get; }

        string FilePath { get; }

        string FileName { get; }



        TResult GetResult<TResult>();
    }
}
