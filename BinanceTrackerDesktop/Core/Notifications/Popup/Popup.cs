using BinanceTrackerDesktop.Core.DirectoryFiles.Control.Images;
using BinanceTrackerDesktop.Core.DirectoryFiles.Directories;

namespace BinanceTrackerDesktop.Core.Notifications.Popup
{
    public sealed class Popup : IPopup
    {
        public string Title { get; set; }

        public string Message { get; set; }

        public int Timeout { get; set; }

        public Icon Icon { get; set; }

        public Action OnShow { get; set; }

        public Action OnClose { get; set; }

        public Action OnClick { get; set; }



        public static readonly Popup Empty = new Popup
        {
            Title = string.Empty,
            Message = string.Empty,
            Icon = new ApplicationDirectoriesControl().Folders.Resources.Images.GetDirectoryFile(DirectoryImagesControl.RegisteredImages.ApplicationIcon).GetIcon(),
            OnShow = null,
            OnClose = null,
            OnClick = null,
        };
    }
}
