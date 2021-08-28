using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Forms.Tracker.API
{
    public interface IFormControl
    {
        FormWindowState WindowState { get; set; }

        void Show();

        void Hide();

        void Close();

        event EventHandler Activated;

        event FormClosingEventHandler FormClosing;

        event EventHandler Move;
    }
}
