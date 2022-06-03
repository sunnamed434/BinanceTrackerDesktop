using BinanceTrackerDesktop.DirectoryFiles.Controls.Images;
using BinanceTrackerDesktop.DirectoryFiles.Directories;

namespace BinanceTrackerDesktop.Notifications.Popup;

public sealed class Popup : IPopup
{
    public string Title { get; set; }

    public string Message { get; set; }

    public int Timeout { get; set; }

    public Icon Icon { get; set; }

    public Action OnShow { get; set; }

    public Action OnClose { get; set; }

    public Action OnClick { get; set; }



    public static readonly IPopup Empty = new Popup
    {
        Title = string.Empty,
        Message = string.Empty,
        Icon = ApplicationDirectories.Resources.Images.GetDirectoryFile(ImagesDirectoryFilesControl.RegisteredImages.ApplicationIcon).GetIcon(),
        OnShow = null,
        OnClose = null,
        OnClick = null,
    };
}
