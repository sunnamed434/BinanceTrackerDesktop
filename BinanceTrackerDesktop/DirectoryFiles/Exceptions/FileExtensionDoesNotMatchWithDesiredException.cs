namespace BinanceTrackerDesktop.DirectoryFiles.Exceptions;

public sealed class FileExtensionDoesNotMatchWithDesiredException : Exception
{
    public FileExtensionDoesNotMatchWithDesiredException(string message) : base(string.Format("File extension does not match with desired: {0}", message))
    {
    }

    public FileExtensionDoesNotMatchWithDesiredException()
    {
    }
}
