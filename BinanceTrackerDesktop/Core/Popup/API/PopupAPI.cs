using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Popup.API
{
    public interface IPopup
    {
        string Title { get; set; }

        string Message { get; set; }

        int Timeout { get; set; }

        ToolTipIcon Icon { get; set; }

        Action OnShow { get; set; }

        Action OnClose { get; set; }

        Action OnClick { get; set; }
    }

    public class Popup : IPopup
    {
        public string Title { get; set; }

        public string Message { get; set; }

        public int Timeout { get; set; }

        public ToolTipIcon Icon { get; set; }

        public Action OnShow { get; set; }

        public Action OnClose { get; set; }

        public Action OnClick { get; set; }



        public static readonly Popup Empty = new Popup
        {
            Title = string.Empty,
            Message = string.Empty,
            Icon = ToolTipIcon.None,
            OnShow = null,
            OnClose = null,
            OnClick = null,
        };
    }
}
