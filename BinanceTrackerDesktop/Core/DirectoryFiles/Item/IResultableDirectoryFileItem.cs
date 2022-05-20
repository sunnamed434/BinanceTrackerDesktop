namespace BinanceTrackerDesktop.Core.DirectoryFiles.Item
{
    public interface IResultableDirectoryFileItem : IDirectoryFileItem
    {
        object Result { get; }



        TResult GetResult<TResult>();
    }
}
