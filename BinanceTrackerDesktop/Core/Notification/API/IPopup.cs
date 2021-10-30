using BinanceTrackerDesktop.Core.DirectoryFiles;
using System;
using System.Drawing;

namespace BinanceTrackerDesktop.Core.Notification.API
{
    public interface IPopup
    {
        string Title { get; set; }

        string Message { get; set; }

        int Timeout { get; set; }

        Icon Icon { get; set; }

        Action OnShow { get; set; }

        Action OnClose { get; set; }

        Action OnClick { get; set; }
    }

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
            Icon = (Icon)new ApplicationDirectoriesControl().Folders.Resources.Images.GetDirectoryFileAt(DirectoryImagesControl.RegisteredImages.ApplicationIcon).Result,
            OnShow = null,
            OnClose = null,
            OnClick = null,
        };
    }
}
