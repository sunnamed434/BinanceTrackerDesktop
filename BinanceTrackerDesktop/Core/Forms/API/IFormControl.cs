using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Forms.API
{
    public interface IFormControl
    {
        FormWindowState WindowState { get; set; }

        void Show();

        void Hide();

        void Close();

        event EventHandler Activated;

        event EventHandler Move;
    }
}
