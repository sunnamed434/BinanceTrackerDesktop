namespace BinanceTrackerDesktop.Core.DirectoryFiles.Exception
{
    public class FileExtensionDoesNotMatchWithDesiredException : System.Exception
    {
        public FileExtensionDoesNotMatchWithDesiredException(string message) : base(string.Format("File extension does not match with desired: {0}", message))
        {

        }

        public FileExtensionDoesNotMatchWithDesiredException()
        {

        }
    }
}
